//
//  MapDetailView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI
import MapKit

struct MapDetailView: View {
    @EnvironmentObject var locationManager: LocationManager
    @StateObject var poiViewModel = POIViewModel()
    @State private var searchText = ""
    @Binding var returnedPoi: POI
    @Environment(\.dismiss) private var dismiss
    
    var body: some View {
        NavigationView {
            List(poiViewModel.pois) { poi in
                VStack(alignment: .leading) {
                    Text(poi.name)
                        .font(.title2)
                    Text(poi.address)
                        .font(.callout)
                }
                .onTapGesture {
                    returnedPoi = poi
                    dismiss()
                }
            }
            .searchable(text: $searchText)
            .onChange(of: searchText, perform: { text in
                if !text.isEmpty {
                    poiViewModel.search(text: text, region: locationManager.region)
                } else {
                    poiViewModel.pois = []
                }
            })
            .toolbar {
                ToolbarItem(placement: .automatic) {
                    Button("Dismiss") {
                        dismiss()
                    }
                }
            }
        }
    }
}

//struct MapDetailView_Previews: PreviewProvider {
//    static var previews: some View {
//        MapDetailView(returnedPoi: .constant(POI(mapItem: MKMapItem())))
//            .environmentObject(LocationManager())
//    }
//}
