//
//  LogInView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI
import Firebase
import FirebaseAuth

struct LogInView: View {
    
    @EnvironmentObject var locationManager: LocationManager
    @ObservedObject var logInViewModel: LogInViewModel
    @State private var email = ""
    @State private var password = ""
    
    init() {
        let initialUserData = UserData(email: "", password: "")
        let path = NavigationPath()
        self.logInViewModel = LogInViewModel(userData: initialUserData, path: path)
    }
    
    var body: some View {
        NavigationStack (path: $logInViewModel.path) {
            
            Image(systemName: "soccerball.inverse")
                .font(.system(size: 80))
                .foregroundColor(.blue)
            Text("Sports Social")
                .font(Font.custom("Baskerville-Bold", size: 26))
                .foregroundColor(.blue)
            Spacer()
            
            TextField("Enter email address", text: $email)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(/*@START_MENU_TOKEN@*/.all/*@END_MENU_TOKEN@*/)
                .autocapitalization(.none)
                .keyboardType(.emailAddress)
                .autocorrectionDisabled()
            
            SecureField("Enter password", text: $password)
                .textFieldStyle(RoundedBorderTextFieldStyle())
                .padding(/*@START_MENU_TOKEN@*/.all/*@END_MENU_TOKEN@*/)
                .autocapitalization(.none)
                .submitLabel(.done)
            
            Button(action: {
                logInViewModel.logIn(email: email, password: password, locationManager: locationManager)
            }) {
                Text("Log In")
            }
            .navigationDestination(for: String.self) { view in
                if view == "HomeScreenView" {
                    MainScreenView().navigationBarBackButtonHidden(true).environmentObject(locationManager)
                }
            }
            Text("Don't have an account?")
                .padding()
            NavigationLink(destination: SignUpView()) {
                Text("Sign Up")
            }
            Spacer()
        }
        .alert(logInViewModel.alertMessage, isPresented: $logInViewModel.showingAlert) {
            Button("OK", role: .cancel) {}
        }
    }
}

struct LogInView_Previews: PreviewProvider {
    static var previews: some View {
        LogInView()
    }
}
