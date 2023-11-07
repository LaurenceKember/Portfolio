//
//  ChatModel.swift
//  SportsSocial
//
//  Created by Laurence Kember on 10/08/2023.
//

import Foundation

struct Message: Identifiable, Equatable {
    var id = UUID()
    let senderUID: String
    let content: String
}
