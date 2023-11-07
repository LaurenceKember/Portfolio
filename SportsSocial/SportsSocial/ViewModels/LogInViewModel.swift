//
//  LogInViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 29/06/2023.
//

import Foundation
import SwiftUI
import FirebaseCore
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class LogInViewModel: ObservableObject {
    
    @Published var userData: UserData
    @Published var errorMessage: String = ""
    @Published var path: NavigationPath
    @Published var alertMessage = ""
    @Published var showingAlert = false
    
    init(userData: UserData, path: NavigationPath) {
        self.userData = userData
        self.path = path
    }
    
    func logIn(email: String, password: String, locationManager: LocationManager) {
        Auth.auth().signIn(withEmail: email, password: password) { [weak self] result, error in
            guard let self = self else {return}
            
            if let error = error {
                print("Log In error: \(error.localizedDescription)")
                alertMessage = "Log In error: \(error.localizedDescription)"
                showingAlert = true
            }
            else {
                print("Log In Successful")
                if let user = result?.user {
                    let uid = user.uid
                    
                    let db = Firestore.firestore()
                    let userRef = db.collection("Users").document(uid)
                    
                    let location = GeoPoint(latitude: locationManager.location?.coordinate.latitude ?? 0.0, longitude: locationManager.location?.coordinate.longitude ?? 0.0)
                    
                    userRef.updateData(["location": location]) { error in
                        if let error = error {
                            print("Error updating location: \(error.localizedDescription)")
                        } else {
                            print("Location updated")
                            self.path.append("HomeScreenView")
                        }
                    }
                }
            }
        }
    }
}
