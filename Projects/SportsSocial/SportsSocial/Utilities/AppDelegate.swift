//
//  AppDelegate.swift
//  SportsSocial
//
//  Created by Laurence Kember on 21/06/2023.
//

import Foundation
import SwiftUI
import UIKit
import FirebaseCore
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

class AppDelegate: NSObject, UIApplicationDelegate {
    func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        // Configure Firebase
        FirebaseApp.configure()
        let db = Firestore.firestore()
        return true
    }
}
