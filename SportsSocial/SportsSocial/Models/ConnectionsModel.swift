//
//  FriendsModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 02/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

struct Connections: Identifiable {
    var name: String
    var selectedSports: [String]
    var id: String
}
