//
//  SignUp1Model.swift
//  SportsSocial
//
//  Created by Laurence Kember on 29/06/2023.
//

import Foundation
import SwiftUI
import FirebaseFirestoreSwift
import FirebaseFirestore

struct SignUpData: Identifiable, Codable {
    @DocumentID var id: String?
    var name: String
    var location: GeoPoint?
    
    var dictionary: [String: Any] {
        return ["name": name, "location": location as Any]
    }
}
