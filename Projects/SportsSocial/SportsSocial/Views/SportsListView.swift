//
//  SignUp2View.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import Foundation
import SwiftUI
import FirebaseCore
import FirebaseFirestoreSwift

struct SportsListView: View {
    
    @StateObject var sportsListViewModel = SportsListViewModel()
    @State private var selectedSports = Set<String>()
    @State private var editMode: EditMode = .active
        
    var body: some View {
        NavigationView {
            List(sportsListViewModel.sports, selection: $selectedSports) { sport in
                Text(sport.sport_name)
            }
            .navigationTitle("Sports")
            .environment(\.editMode, $editMode)
            .toolbar {
                NavigationLink(destination: MainScreenView().navigationBarBackButtonHidden(true)) {
                    Text("Sign Up")
                }
            }
        }
        .alert(sportsListViewModel.alertMessage, isPresented: $sportsListViewModel.showingAlert) {
            Button("OK", role: .cancel) {}
        }
        .onAppear {
            sportsListViewModel.fetchSports()
        }
        .onChange(of: selectedSports) { newSelectedSports in
            print("Selected Sports: \(newSelectedSports)")
            sportsListViewModel.uploadSelectedSports(selectedSports: newSelectedSports)
        }
    }
}


struct SportsListView_Previews: PreviewProvider {
    static var previews: some View {
        SportsListView()
    }
}
