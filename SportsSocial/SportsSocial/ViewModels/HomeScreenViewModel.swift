//
//  HomeScreenViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 17/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class HomeScreenViewModel: ObservableObject {
    
    @Published var activities: [Activity] = []
    var userRefs: [String] = []
    
    let db = Firestore.firestore()
    
    func getFollowingRefs(completion: @escaping (Bool) -> Void) {
        guard let userID = Auth.auth().currentUser?.uid else {
            completion(true)
            return
        }
        db.collection("Users").document(userID).getDocument { document, error in
            if let error = error {
                print("Error getting following users: \(error)")
                completion(false)
                return
            }
            
            guard let data = document?.data(),
                  let followingUserRefs = data["following"] as? [String] else {
                print("No users followed")
                completion(false)
                return
            }
            
            self.userRefs = followingUserRefs
            completion(true)
        }
    }
    
    func getUser1Activities(completion: @escaping (Bool) -> Void) {
        
        let activity1Query = Firestore.firestore().collection("Activities")
            .whereField("user1ID", in: userRefs)
        
        activity1Query.getDocuments { (querySnapshot, error) in
            if let error = error {
                print("Error getting activity documents: \(error)")
                return
            }
            
            guard let documents = querySnapshot?.documents else {
                print("No activity documents found")
                return
            }
            
            var updated1Activities: [Activity] = []
            
            for document in documents {
                let data = document.data()
                
                guard let user1Name = data["user1Name"] as? String,
                      let user2Name = data["user2Name"] as? String,
                      let sportName = data["sportName"] as? String else {
                    continue
                }
                
                let activity = Activity(user1Name: user1Name,
                                        user2Name: user2Name,
                                        sportName: sportName)
                updated1Activities.append(activity)
                
            }
            
            self.activities = updated1Activities
            completion(true)
        }
    }
    
    func getUser2Activities(completion: @escaping (Bool) -> Void) {
        
        let activity2Query = Firestore.firestore().collection("Activities")
            .whereField("user2ID", in: userRefs)
        
        activity2Query.getDocuments { (querySnapshot, error) in
            if let error = error {
                print("Error listening to activity documents: \(error)")
                return
            }
            
            guard let documents = querySnapshot?.documents else {
                print("No activity documents found")
                return
            }

            var updated2Activities: [Activity] = []

            for document in documents {
                let data = document.data()
                
                guard let user1Name = data["user1Name"] as? String,
                let user2Name = data["user2Name"] as? String,
                let sportName = data["sportName"] as? String else {
                    continue
                }
                    
                let activity = Activity(user1Name: user1Name,
                                        user2Name: user2Name,
                                        sportName: sportName)
                updated2Activities.append(activity)
                
            }

            self.activities.append(contentsOf: updated2Activities)
            completion(true)
        }
    }
}
