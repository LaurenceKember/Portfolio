//
//  ProfileViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 22/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class ProfileViewModel: ObservableObject {
    
    @Published var name: String = ""
    @Published var selectedSports: [String] = []
    
    let db = Firestore.firestore()
    
    func getUserDetails() {
        
        guard let userID = Auth.auth().currentUser?.uid else {
            return
        }
        db.collection("Users").document(userID).getDocument { document, error in
            if let error = error {
                print("Error getting following users: \(error)")
                return
            }
            
            if let data = document?.data() {
                if let userName = data["name"] as? String {
                    self.name = userName
                }
                if let sportdata = data["selected_sports"] as? [String] {
                    self.selectedSports = sportdata
                }
            }
        }
    }
}
