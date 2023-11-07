//
//  FriendsView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI
import Firebase
import FirebaseFirestore

struct ConnectionsView: View {
    
    @StateObject private var connectionsViewModel = ConnectionsViewModel()
    
    var body: some View {
        NavigationView {
            List(connectionsViewModel.connections, id: \.name) { connection in
                VStack(alignment: .leading) {
                    HStack {
                        Text(connection.name)
                            .font(Font.custom("Baskerville-Bold", size: 18))
                            .foregroundColor(.blue)
                        
                        Spacer()
                        
                        Button(action: {
                            connectionsViewModel.followUser(userToFollowID: connection.id)
                        }) {
                            Text("Follow")
                                .foregroundColor(.white)
                                .padding(.horizontal, 10)
                                .padding(.vertical, 5)
                                .background(Color.blue)
                                .cornerRadius(5)
                        }
                    }
                    Text("Favourite Sports: \(connection.selectedSports.joined(separator: ", "))")
                        .font(Font.custom("Baskerville-Bold", size: 15))
                        .foregroundColor(.blue)
                }
            }
            .navigationTitle("Connections")
            .onAppear {
                connectionsViewModel.connections = []
                connectionsViewModel.getUserLocation { success in
                    if success {
                        connectionsViewModel.getUserSelectedSports { success in
                            if success {
                                connectionsViewModel.findMatchedUsers(selectedSports: connectionsViewModel.selectedSports)
                            } else {
                                print("Failed to fetch user's selected sports.")
                            }
                        }
                    } else {
                        print("Failed to fetch user's location.")
                    }
                }
            }
        }
    }
}

struct ConnectionsView_Previews: PreviewProvider {
    static var previews: some View {
        ConnectionsView()
    }
}
