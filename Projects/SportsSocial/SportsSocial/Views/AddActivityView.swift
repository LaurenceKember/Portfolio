//
//  AddActivityView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI

struct AddActivityView: View {
    
    @Environment(\.dismiss) private var dismiss
    @ObservedObject var activityViewModel = ActivityViewModel()
    
    var body: some View {
        NavigationView {
            VStack {
                VStack {
                    Text("Select Sport")
                        .font(.headline)
                    Picker("Select Sport", selection: $activityViewModel.selectedSport) {
                        ForEach(activityViewModel.sports, id: \.self) { sport in
                            Text(sport)
                        }
                    }
                }
                .padding()
                VStack {
                    Text("Select user")
                        .font(.headline)
                    Picker("Select User", selection: $activityViewModel.selectedUser) {
                        ForEach(activityViewModel.users, id: \.self) { user in
                            Text(user)
                        }
                    }
                }
                .padding()
                Button("Add Activity") {
                    activityViewModel.addActivityData()
                    dismiss()
                }
                .padding()
            }
            .navigationTitle("Add Activity")
            .toolbar {
                ToolbarItem(placement: .automatic) {
                    Button("Dismiss") {
                        dismiss()
                    }
                }
            }
        }
        .onAppear {
            activityViewModel.getSports()
            activityViewModel.getFollowingRefs()
        }
    }
}

//struct AddActivityView_Previews: PreviewProvider {
//    static var previews: some View {
//        AddActivityView()
//    }
//}
