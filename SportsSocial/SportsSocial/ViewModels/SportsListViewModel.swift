//
//  SportsListViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 19/07/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class SportsListViewModel: ObservableObject {
    
    @Published var sports: [Sports] = []
    @Published var alertMessage = ""
    @Published var showingAlert = false
    
    func fetchSports() {
        let sportsCollection = Firestore.firestore().collection("Sports")
        sportsCollection.getDocuments() { (querySnapshot, err) in
            if let err = err {
                print("Error getting documents: \(err)")
            } else {
                var sportData: [Sports] = []
                for document in querySnapshot!.documents {
                    if let sportName = document.data()["sport_name"] as? String {
                        let sport = Sports(id: document.documentID, sport_name: sportName)
                        sportData.append(sport)
                    }
//                    print("\(document.documentID) => \(document.data())")
                }
                DispatchQueue.main.async {
                    self.sports = sportData
//                    print(self.sports)
                }
                
            }
        }
    }
    
    func uploadSelectedSports(selectedSports: Set<String>) {
        guard let currentUserUID = Auth.auth().currentUser?.uid else {
            // User not authenticated or UID not available
            return
        }

        let selectedSportNames = selectedSports.map { sportID in
            return sports.first { $0.id == sportID }?.sport_name ?? ""
        }

        let userProfileRef = Firestore.firestore().collection("Users").document(currentUserUID)

        userProfileRef.setData(["selected_sports": selectedSportNames], merge: true) { err in
            if let err = err {
                print("Error updating selected sports: \(err)")
            } else {
                print("Selected sports updated successfully!")
            }
        }
    }
}
