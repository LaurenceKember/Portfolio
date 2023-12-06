//
//  SignUp1ViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 29/06/2023.
//

import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth
import MapKit

class SignUpViewModel: ObservableObject {
    
//    @EnvironmentObject var locationManager: LocationManager
    @Published var signUpData: SignUpData
    @Published var errorMessage: String = ""
    @Published var success = false
    @Published var alertMessage = ""
    @Published var showingAlert = false
    
    init(signUpData: SignUpData) {
        self.signUpData = signUpData
    }
    
    func register(name: String, email: String, password: String, locationManager: LocationManager) {
        signUpData.name = name
        
        Auth.auth().createUser(withEmail: email, password: password) { [weak self] result, error in
            guard let self = self else { return }
            
            if let error = error {
                print("Registration error: \(error.localizedDescription)")
                alertMessage = "Registration error: \(error.localizedDescription)"
                showingAlert = true
            } else if let user = result?.user {
                
                let uid = user.uid
                
                let location = GeoPoint(latitude: locationManager.location?.coordinate.latitude ?? 0.0, longitude: locationManager.location?.coordinate.longitude ?? 0.0)
                
                let userRef = Firestore.firestore().collection("Users").document(uid)
                
                let userData: [String: Any] = [
                    "name": self.signUpData.name,
                    "location": location]
                
                userRef.setData(userData) { error in
                    if let error = error {
                        print("Error creating user profile: \(error.localizedDescription)")
                    } else {
                        print("User profile created")
                        self.success = true
                    }
                }
            }
        }
    }
}
    
//    self.createUserName(uid: user.uid, name: self.signUpData.name) { success in
    //                    if success {
    //                        self.success = true
    //                    }
    
//    func createUserName(uid: String, name: String, completion: @escaping (Bool) -> Void) {
//        let userRef = Firestore.firestore().collection("Users").document(uid)
//
//        let userData: [String: Any] = [
//            "name": name
//        ]
//
//        userRef.setData(userData) { error in
//            if let error = error {
//                print("Error creating user profile: \(error.localizedDescription)")
//                completion(false)
//            } else {
//                print("User profile created successfully")
//                completion(true)
//            }
//        }
//    }


