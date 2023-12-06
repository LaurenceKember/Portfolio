//
//  MapView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI
import MapKit

struct MapView: View {
    
    @EnvironmentObject var locationManager: LocationManager
    @State private var showPoiLookUpSheet = false
    @State var returnedPoi = POI(mapItem: MKMapItem())
    @State private var mapRegion = MKCoordinateRegion()
    @State var mapSize = 10000.00
    
    var body: some View {
        NavigationView {
            VStack {
                Map(coordinateRegion: $mapRegion, interactionModes: .all, showsUserLocation: true, userTrackingMode: .constant(.follow), annotationItems: [returnedPoi]) { poi in
                    MapMarker(coordinate: poi.coordinate)
                }
                .frame(width: UIScreen.main.bounds.width, height: UIScreen.main.bounds.height * 0.7)
                .padding()
                
                Button {
                    showPoiLookUpSheet.toggle()
                } label: {
                    Image(systemName: "magnifyingglass")
                    Text("Search")
                }
            }
            .padding()
            .fullScreenCover(isPresented: $showPoiLookUpSheet) {
                MapDetailView(returnedPoi: $returnedPoi)
            }
        }
    }
}

struct MapView_Previews: PreviewProvider {
    static var previews: some View {
        MapView()
            .environmentObject(LocationManager())
    }
}
