//
//  ChatListView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI
import Firebase
import FirebaseFirestore

struct ChatListView: View {
    
    @StateObject private var chatListViewModel = ChatListViewModel()
    
    var body: some View {
        NavigationView {
            List(chatListViewModel.chats, id: \.id) { chat in
                NavigationLink(destination: ChatView(chat: chat)) {
                    Text(chat.name)
                        .font(Font.custom("Baskerville-Bold", size: 18))
                        .foregroundColor(.blue)
                }
            }
            .navigationTitle("Chats")
            .onAppear {
                chatListViewModel.chats = []
                chatListViewModel.fetchFollowers { success in
                    if success {
                        chatListViewModel.fetchFollowing { success in
                            if success {
                                chatListViewModel.fetchCommonUsersNames()
                            }
                        }
                    }
                }
            }
        }
    }
}

struct ChatListView_Previews: PreviewProvider {
    static var previews: some View {
        ChatListView()
    }
}
