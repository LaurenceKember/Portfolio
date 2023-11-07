//
//  SplashView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 20/06/2023.
//

import SwiftUI
import CoreData

struct SplashView: View {
    @EnvironmentObject var locationManager: LocationManager
    @Environment(\.managedObjectContext) private var viewContext
    @State private var isActive = false
    @State private var size = 0.8
    @State private var opacity = 0.5
    
    var body: some View {
        if isActive == true {
            LogInView().environmentObject(locationManager)
        }
        else {
            VStack {
                VStack {
                    Image(systemName: "soccerball.inverse")
                        .font(.system(size: 80))
                        .foregroundColor(.blue)
                    Text("Sports Social")
                        .font(Font.custom("Baskerville-Bold", size: 26))
                        .foregroundColor(.blue)
                }
                .scaleEffect(size)
                .opacity(opacity)
                .onAppear {
                    withAnimation(.easeIn(duration: 1.2)) {
                        self.size = 0.9
                        self.opacity = 1.0
                    }
                }
            }
            .onAppear {
                DispatchQueue.main.asyncAfter(deadline: .now() + 2.0) {
                    withAnimation {
                        self.isActive = true
                    }
                }
            }
        }
        
    }
}

struct SplashView_Previews: PreviewProvider {
    static var previews: some View {
        SplashView()
//            .environment(\.managedObjectContext, PersistenceController.preview.container.viewContext)
            .environmentObject(LocationManager())
    }
}
