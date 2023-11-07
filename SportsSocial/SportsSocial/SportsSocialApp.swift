//
//  SportsSocialApp.swift
//  SportsSocial
//
//  Created by Laurence Kember on 20/06/2023.
//

import SwiftUI
import FirebaseCore
import FirebaseFirestoreSwift
import FirebaseAuth

@main
struct SportsSocialApp: App {
    @StateObject var locationManager = LocationManager()
    @UIApplicationDelegateAdaptor(AppDelegate.self) var delegate

    var body: some Scene {
        WindowGroup {
                SplashView()
                    .environmentObject(locationManager)
        }
    }
}
