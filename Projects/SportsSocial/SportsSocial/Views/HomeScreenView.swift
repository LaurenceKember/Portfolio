//
//  HomeScreenView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI

struct HomeScreenView: View {
    
    @StateObject var homeScreenViewModel = HomeScreenViewModel()
    @State private var showAddActivity = false
    @State private var isLoading = true
    
    var body: some View {
        
        NavigationView {
            VStack {
                Image(systemName: "soccerball.inverse")
                    .font(.system(size: 60))
                    .foregroundColor(.blue)
                Text("Sports Social")
                    .font(Font.custom("Baskerville-Bold", size: 19.5))
                    .foregroundColor(.blue)
                
                if isLoading {
                    ProgressView("Loading activities")
                } else {
                    List(homeScreenViewModel.activities, id: \.id) { activity in
                            VStack {
                                HStack {
                                    Text("Sport Played:")
                                        .font(Font.custom("Baskerville-Bold", size: 15))
                                        .foregroundColor(.blue)
                                    Text(activity.sportName)
                                        .font(Font.custom("Baskerville-Bold", size: 15))
                                        .foregroundColor(.blue)
                                }
                                VStack {
                                    HStack {
                                        Text("Player 1: ")
                                            .font(Font.custom("Baskerville-Bold", size: 15))
                                            .foregroundColor(.blue)
                                        Text(activity.user1Name)
                                            .font(Font.custom("Baskerville-Bold", size: 15))
                                            .foregroundColor(.blue)
                                    }
                                    HStack {
                                        Text("Player 2: ")
                                            .font(Font.custom("Baskerville-Bold", size: 15))
                                            .foregroundColor(.blue)
                                        Text(activity.user2Name)
                                            .font(Font.custom("Baskerville-Bold", size: 15))
                                            .foregroundColor(.blue)
                                    }
                                }
                            }
                        }
                    }
                Spacer()
                HStack {
                    Spacer()
                    Button {
                        showAddActivity.toggle()
                    } label: {
                        Image(systemName: "plus.circle")
                            .resizable()
                            .frame(width: 50, height: 50)
                            .foregroundColor(.blue)
                    }
                }
                .padding(.trailing, 16)
                .padding(.bottom, 16)
                .fullScreenCover(isPresented: $showAddActivity) {
                    AddActivityView()
                    }
                }
            }
            .onAppear {
                isLoading = true
                homeScreenViewModel.getFollowingRefs { success in
                    if success {
                        homeScreenViewModel.getUser1Activities { success in
                            if success {
                                homeScreenViewModel.getUser2Activities { success in
                                    if success {
                                        isLoading = false
                                    }
                            }
                        }
                    }
                }
            }
        }
    }
}


//struct HomeScreenView_Previews: PreviewProvider {
//    static var previews: some View {
//        HomeScreenView()
//            .environmentObject(LocationManager: locationManager)
//    }
//}
