//
//  ActivityViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 18/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class ActivityViewModel: ObservableObject {
    
    @Published var activties: [Activity] = []
    @Published var sports: [String] = []
    @Published var users: [String] = []
    var userRefs: [String] = []
    @Published var selectedSport: String = ""
    @Published var selectedUser: String = ""
    var user1ID: String = ""
    var user1Name: String = ""
    var user2ID: String = ""
    var user2Name: String = ""
    
    let db = Firestore.firestore()
    
    func getSports() {
        db.collection("Sports").getDocuments { querySnapshot, error in
            if let error = error {
                print("Error getting sports: \(error)")
                return
            }
            guard let documents = querySnapshot?.documents else {
                print("No documents")
                return
            }
            self.sports = documents.compactMap { document in
                let data = document.data()
                return data["sport_name"] as? String
            }
        }
    }
    
    func getFollowingRefs() {
        guard let userID = Auth.auth().currentUser?.uid else {
            return
        }
        db.collection("Users").document(userID).getDocument { document, error in
            if let error = error {
                print("Error getting following users: \(error)")
                return
            }
            
            guard let data = document?.data(),
                  let followingUserRefs = data["following"] as? [String] else {
                print("No users followed")
                return
            }
            
            self.userRefs = followingUserRefs
            self.user1ID = userID
            self.getUserNames()
        }
    }
    
    func getUserNames() {
        for userReference in userRefs {
            db.collection("Users").document(userReference).getDocument { document, error in
                if let document = document, document.exists, let name = document.data()?["name"] as? String {
                    self.users.append(name)
                } else {
                    print("Failed to access user document")
                }
            }
        }
    }
    
    func addActivityData() {
        for userReference in userRefs {
            db.collection("Users").document(userReference).getDocument { document, error in
                if let document = document, document.exists, let name = document.data()?["name"] as? String {
                    if name == self.selectedUser {
                        self.user2Name = self.selectedUser
                        self.user2ID = userReference
                        
                        self.db.collection("Users").document(self.user1ID).getDocument { document, error in
                            if let document = document, document.exists, let name = document.data()?["name"] as? String {
                                self.user1Name = name
                                self.addActivity()
                            }
                        }
                    }
                }
            }
        }
    }
    
    func addActivity() {
        let activityData: [String: Any] = [
            "sportName": selectedSport,
            "user1ID": user1ID,
            "user1Name": user1Name,
            "user2ID": user2ID,
            "user2Name": user2Name,
            "timestamp": Timestamp()
        ]
        
        db.collection("Activities").addDocument(data: activityData) { error in
            if let error = error {
                print("Error adding activity: \(error)")
            } else {
                print("Activity added succcessfully")
            }
        }
    }
}
