//
//  MainScreenView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 22/08/2023.
//

import SwiftUI

struct MainScreenView: View {
    
    @EnvironmentObject var locationManager: LocationManager
    
    var body: some View {
        TabView {
            HomeScreenView()
                .tabItem {
                    Image(systemName: "newspaper.fill")
                }
            ConnectionsView()
                .tabItem {
                    Image(systemName: "person.2.fill")
                }
            ChatListView()
                .tabItem {
                    Image(systemName: "bubble.left.and.bubble.right.fill")
                }
            MapView()
                .environmentObject(locationManager)
                .tabItem {
                    Image(systemName: "map.fill")
                }
            ProfileView()
                .tabItem {
                    Image(systemName: "person.fill")
                }
        }
    }
}

//struct MainScreenView_Previews: PreviewProvider {
//    static var previews: some View {
//        MainScreenView()
//    }
//}
