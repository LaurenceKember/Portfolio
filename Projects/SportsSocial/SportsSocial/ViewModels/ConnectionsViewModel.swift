//
//  FriendsViewModler.swift
//  SportsSocial
//
//  Created by Laurence Kember on 02/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth
import MapKit

class ConnectionsViewModel: ObservableObject {
    
    @Published var connections: [Connections] = []
    @Published var followStatus: [String: Bool] = [:]
    var selectedSports: [String] = []
    var userLatitude: Double = 0.0
    var userLongitude: Double = 0.0
    var maxDistance = 5000.0
    
    
    let db = Firestore.firestore()
    
    func getUserLocation(completion: @escaping (Bool) -> Void) {
        if let userUID = Auth.auth().currentUser?.uid {
            let userRef = db.collection("Users").document(userUID)
            
            userRef.getDocument { document, error in
                if let error = error {
                    print("Error fetching user document: \(error)")
                    completion(false)
                    return
                }
                
                if let document = document, document.exists {
                    if let userLocation = document.get("location") as? GeoPoint {
                        self.userLatitude = userLocation.latitude
                        self.userLongitude = userLocation.longitude
                        completion(true)
                    } else {
                        print("Error: No 'location' field or invalid format.")
                        completion(false)
                    }
                } else {
                    print("Error: User document not found location.")
                    completion(false)
                }
            }
        } else {
            print("Error: User not signed in.")
            completion(false)
        }
    }
    
    func getUserSelectedSports(completion: @escaping (Bool) -> Void) {
        if let userUID = Auth.auth().currentUser?.uid {
            let userRef = db.collection("Users").document(userUID)
            
            userRef.getDocument { document, error in
                if let error = error {
                    print("Error fetching user document: \(error)")
                    completion(false)
                    return
                }
                
                if let document = document, document.exists {
                    if let userSelectedSports = document.get("selected_sports") as? [String] {
                        self.selectedSports = userSelectedSports
                        completion(true)
                    } else {
                        print("Error: No 'selected_sports' field or invalid format.")
                        completion(false)
                    }
                } else {
                    print("Error: User document not found selectedsports.")
                    completion(false)
                }
            }
        } else {
            print("Error: User not signed in.")
            completion(false)
        }
    }
    
    func findMatchedUsers(selectedSports: [String]) {
        
        let usersCollection = db.collection("Users")
        
        usersCollection
            .whereField("selected_sports", arrayContainsAny: selectedSports)
            .getDocuments() { (querySnapshot, error) in
                if let error = error {
                    print("Error fetching nearby users based on latitude: \(error)")
                } else {
                    let currentUserID = Auth.auth().currentUser?.uid
                    
                    for document in querySnapshot!.documents {
                        if let geopoint = document.data()["location"] as? GeoPoint,
                           document.documentID != currentUserID {
                            let documentLatitude = geopoint.latitude
                            let documentLongitude = geopoint.longitude
                            
                            let distance = self.calculateDistance(latitude1: self.userLatitude, longitude1: self.userLongitude, latitude2: documentLatitude, longitude2: documentLongitude)
                            
                            if distance <= self.maxDistance {
                                if let name = document.data()["name"] as? String,
                                   let selectedSports = document.data()["selected_sports"] as? [String] {
                                    let connection = Connections(name: name, selectedSports: selectedSports, id: document.documentID)
                                    self.connections.append(connection)
                                }
                            }
                        }
                    }
            }
        }
    }
    
    func calculateDistance(latitude1: Double, longitude1: Double, latitude2: Double, longitude2: Double) -> Double {
        let coordinate1 = CLLocation(latitude: latitude1, longitude: longitude1)
        let coordinate2 = CLLocation(latitude: latitude2, longitude: longitude2)
        
        return coordinate1.distance(from: coordinate2)
    }
    
    func followUser(userToFollowID: String) {
        guard let currentUserUID = Auth.auth().currentUser?.uid else {
            print("Current user UID is nil.")
            return
        }
        
        // Update current user's document
        db.collection("Users").document(currentUserUID).updateData([
            "following": FieldValue.arrayUnion([userToFollowID])
        ])
        
        // Update target user's document
        db.collection("Users").document(userToFollowID).updateData([
            "followers": FieldValue.arrayUnion([currentUserUID])
        ])
    }
}
