//
//  ChatListModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 09/08/2023.
//

import Foundation
import SwiftUI
import Firebase
import FirebaseFirestore
import FirebaseFirestoreSwift
import FirebaseAuth

struct Chats: Identifiable {
    var name: String
    var id: String
}
