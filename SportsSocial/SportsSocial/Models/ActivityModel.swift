//
//  ActivitiesModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 17/08/2023.
//

import Foundation
import Firebase

struct Activity: Identifiable, Equatable {
    var id = UUID()
    let user1Name: String
    let user2Name: String
    let sportName: String
}
