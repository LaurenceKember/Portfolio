//
//  POIViewModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 26/07/2023.
//

import Foundation
import MapKit

@MainActor
class POIViewModel: ObservableObject {
    @Published var pois: [POI] = []
    
    func search(text: String, region: MKCoordinateRegion) {
        let searchRequest = MKLocalSearch.Request()
        searchRequest.naturalLanguageQuery = text
        searchRequest.region = region
        let search = MKLocalSearch(request: searchRequest)
        
        search.start { response, error in
            guard let response = response else {
                print("Error: \(error?.localizedDescription ?? "Unknown Error")")
                return
            }
            
            self.pois = response.mapItems.map(POI.init)
        }
    }
}
