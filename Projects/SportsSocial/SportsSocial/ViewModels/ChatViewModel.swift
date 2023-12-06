//
//  ChatViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 10/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class ChatViewModel: ObservableObject {
    
    @Published var chat: Chats
    @Published var messages: [Message] = []
    var chatDocumentID: String
    @Published var content: String = ""
    var senderName: String = ""
    
    init(chat: Chats) {
        self.chat = chat
        self.chatDocumentID = ""
    }
    
    func checkChats() {
        guard let user1UID = Auth.auth().currentUser?.uid else {
            return
        }
        let user2UID = chat.id

        let chatQuery = Firestore.firestore().collection("Chats")
            .whereField("user1UID", in: [user1UID, user2UID])
            .whereField("user2UID", in: [user1UID, user2UID])

        chatQuery.getDocuments { (querySnapshot, error) in
            if let error = error {
                print("Error fetching chat documents: \(error)")
                return
            }

            if let documents = querySnapshot?.documents, !documents.isEmpty {
                // Chat document already exists
                let existingChatDocument = documents[0]
                self.chatDocumentID = existingChatDocument.documentID
                self.loadMessages()
            } else {
                // Chat document doesn't exist, create a new one
                self.createNewChatDocument(user1UID: user1UID, user2UID: user2UID)
            }
        }
    }
    
    func createNewChatDocument(user1UID: String, user2UID: String) {
        let newChatData: [String: Any] = [
            "user1UID": user1UID,
            "user2UID": user2UID
        ]
        
        let chatsCollectionRef = Firestore.firestore().collection("Chats")
        let newChatDocumentRef = chatsCollectionRef.addDocument(data: newChatData) { error in
            if let error = error {
                print("Error creating chat document: \(error)")
                return
            }
        }
        self.chatDocumentID = newChatDocumentRef.documentID
        // Create the messages subcollection under the new chat document
        newChatDocumentRef.collection("messages").addDocument(data: [:]) { error in
            if let error = error {
                print("Error creating messages subcollection: \(error)")
                return
            } else {
                self.loadMessages()
            }
        }
    }

    func sendMessage() {
        
        guard let senderUID = Auth.auth().currentUser?.uid else {
            return
        }
        
        let messageData: [String: Any] = [
            "senderUID": senderUID,
            "timestamp": Timestamp(),
            "content": content
        ]
        
        let messagesCollectionRef = Firestore.firestore()
            .collection("Chats")
            .document(chatDocumentID)
            .collection("messages")
        
        messagesCollectionRef.addDocument(data: messageData) { error in
            if let error = error {
                print("Error sending message: \(error)")
                return
            }
        }
        content = ""
    }
    
    func loadMessages() {
        let db = Firestore.firestore()
        db.collection("Chats").document(chatDocumentID).collection("messages")
            .order(by: "timestamp")
            .addSnapshotListener { querySnapshot, error in
                guard let documents = querySnapshot?.documents else {
                    print("Error fetching documents: \(error?.localizedDescription ?? "Unknown error")")
                    return
                }

                self.messages = documents.compactMap { document in
                    let data = document.data()
                    let senderUID = data["senderUID"] as? String ?? ""
                    let content = data["content"] as? String ?? ""
                    if senderUID == Auth.auth().currentUser?.uid {
                        let senderName = "Me"
                        return Message(senderUID: senderName, content: content)
                    } else {
                        let senderName = self.chat.name
                        return Message(senderUID: senderName, content: content)
                    }
                }
            }
    }
}
