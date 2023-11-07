//
//  ChatListViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 09/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class ChatListViewModel: ObservableObject {
    
    @Published var chats: [Chats] = []
    @Published var followers: [String] = []
    @Published var following: [String] = []
    var commonUsers: [String] {
        return followers.filter { following.contains($0)}
    }
    
    let db = Firestore.firestore()
    
    func fetchFollowers(completion: @escaping (Bool) -> Void) {
        guard let currentUserID = Auth.auth().currentUser?.uid else {
            return
        }
        
        let userDocRef = db.collection("Users").document(currentUserID)
        
        userDocRef.getDocument { document, error in
            if let document = document, document.exists {
                if let followersArray = document.data()?["followers"] as? [String] {
                    self.followers = followersArray
                    completion(true)
                }
            } else {
                print("Document does not exist")
                completion(false)
            }
        }
    }
    
    func fetchFollowing(completion: @escaping (Bool) -> Void) {
        guard let currentUserID = Auth.auth().currentUser?.uid else {
            return
        }
        
        let userDocRef = db.collection("Users").document(currentUserID)
        
        userDocRef.getDocument { document, error in
            if let document = document, document.exists {
                if let followingArray = document.data()?["following"] as? [String] {
                    self.following = followingArray
                    completion(true)
                }
            } else {
                print("Document does not exist")
                completion(false)
            }
        }
    }
    
    func fetchCommonUsersNames() {
        for userReference in commonUsers {
            db.collection("Users").document(userReference).getDocument { document, error in
                if let document = document, document.exists, let name = document.data()?["name"] as? String {
                    let chat = Chats(name: name, id: userReference)
                    self.chats.append(chat)
                } else {
                    print("Failed to access user document")
                }
            }
        }
    }
}
