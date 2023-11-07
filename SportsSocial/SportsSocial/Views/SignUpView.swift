//
//  SignUp1View.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import Foundation
import SwiftUI
import FirebaseCore
import FirebaseFirestoreSwift

struct SignUpView: View {
    
    @EnvironmentObject var locationManager: LocationManager
    @ObservedObject var signUpViewModel: SignUpViewModel
    @State private var name = ""
    @State private var email = ""
    @State private var password = ""
    
    init() {
        let initialUserData = SignUpData(name: "")
        self.signUpViewModel = SignUpViewModel(signUpData: initialUserData)
    }
    
    var body: some View {
        NavigationStack {
            Image(systemName: "soccerball.inverse")
                .font(.system(size: 80))
                .foregroundColor(.blue)
            Text("Sports Social")
                .font(Font.custom("Baskerville-Bold", size: 26))
                .foregroundColor(.blue)
            Spacer()
            
            Text("Please enter your details below")
                .padding()
            TextField("Full name", text: $name)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(/*@START_MENU_TOKEN@*/.all/*@END_MENU_TOKEN@*/)
                .disableAutocorrection(true)
            TextField("Enter email address", text: $email)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(/*@START_MENU_TOKEN@*/.all/*@END_MENU_TOKEN@*/)
                .autocapitalization(.none)
            SecureField("Enter password", text: $password)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(/*@START_MENU_TOKEN@*/.all/*@END_MENU_TOKEN@*/)
                .autocapitalization(.none)
            Button(action: {
                signUpViewModel.register(name: name, email: email, password: password, locationManager: locationManager)
            }) {
                Text("Sign Up")
            }
            
            Spacer()
        }
        .alert(signUpViewModel.alertMessage, isPresented: $signUpViewModel.showingAlert) {
            Button("OK", role: .cancel) {}
        }
        .background(
            NavigationLink(
                destination: SportsListView().navigationBarBackButtonHidden(true),
                isActive: $signUpViewModel.success,
                label: { EmptyView() }
            )
        )
        
    }
}

struct SignUpView_Previews: PreviewProvider {
    static var previews: some View {
        SignUpView()
    }
}
