//
//  PointsOfInterest.swift
//  SportsSocial
//
//  Created by Laurence Kember on 26/07/2023.
//

import Foundation
import MapKit
import CoreLocation

struct POI: Identifiable, Equatable {
    let id = UUID().uuidString
    var mapItem: MKMapItem
    var coordinate: CLLocationCoordinate2D {
        CLLocationCoordinate2D(latitude: self.mapItem.placemark.coordinate.latitude, longitude: self.mapItem.placemark.coordinate.longitude)
    }
    
    init(mapItem: MKMapItem) {
        self.mapItem = mapItem
    }
    
    var name: String {
        self.mapItem.name ?? ""
    }
    
    var address: String {
        let placemark = self.mapItem.placemark
        var cityAndRegion = ""
        var address = ""
        
        cityAndRegion = placemark.locality ?? "" //city
        if let region = placemark.administrativeArea {
            cityAndRegion = cityAndRegion.isEmpty ? region : "\(cityAndRegion), \(region)"
        }
        
        address = placemark.subThoroughfare ?? "" //address
        if let street = placemark.thoroughfare {
            address = address.isEmpty ? street : "\(address), \(street)"
        }
        
        if address.trimmingCharacters(in: .whitespaces).isEmpty && !cityAndRegion.isEmpty {
            address = cityAndRegion
        } else {
            address = cityAndRegion.isEmpty ? address : "\(address), \(cityAndRegion)"
        }
        
        if let postcode = placemark.postalCode {
            address = address.isEmpty ? postcode : "\(address), \(postcode)"
        }
        
        return address
    }
}
