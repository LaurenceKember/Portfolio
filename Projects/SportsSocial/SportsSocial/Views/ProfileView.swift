//
//  ProfileView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI
import Firebase
import FirebaseAuth

struct ProfileView: View {
    
    @StateObject private var profileViewModel = ProfileViewModel()
    @EnvironmentObject var locationManager: LocationManager
    
    var body: some View {
        
        NavigationView {
            VStack {
                Image(systemName: "person.circle.fill")
                    .font(.system(size: 60))
                Text(profileViewModel.name)
                    .font(.title)
                    .bold()
                    .padding()
                Section(header: Text("My Selected Sports")) {
                    List(profileViewModel.selectedSports, id: \.self) { sport in
                        Text(sport)
                    }
                }
                Button("Sign Out") {
                    do {
                        try Auth.auth().signOut()
                    } catch {
                        print("Error signing out: \(error.localizedDescription)")
                    }
                }
                .padding()
            }
            .onAppear {
                profileViewModel.getUserDetails()
            }
        }
    }
}

struct ProfileView_Previews: PreviewProvider {
    static var previews: some View {
        ProfileView()
    }
}
