//
//  ChatView.swift
//  SportsSocial
//
//  Created by Laurence Kember on 27/06/2023.
//

import SwiftUI

struct ChatView: View {
    
    var chat: Chats
    @ObservedObject private var chatViewModel: ChatViewModel
    
    init(chat: Chats) {
        self.chat = chat
        self.chatViewModel = ChatViewModel(chat: chat)
    }
    
    var body: some View {
        ScrollViewReader {scrollView in
            ScrollView(.vertical) {
                LazyVStack(alignment: .leading) {
                    ForEach(chatViewModel.messages) { message in
                        VStack {
                            HStack {
                                Text(message.content)
                                    .font(.headline)
                                    .frame(alignment: .leading)
                                Spacer()
                            }
                            HStack {
                                Spacer()
                                Text(message.senderUID)
                                    .font(.subheadline)
                                    .frame(alignment: .trailing)
                            }
                        }
                        .padding()
                        .background(LinearGradient(colors: [Color.blue.opacity(0.7), Color.blue.opacity(0.3)], startPoint: .topLeading, endPoint: .bottomTrailing))
                        .cornerRadius(8)
                        .padding(EdgeInsets(top: 4, leading: 8, bottom: 4, trailing: 8))
                    }
                }
                .id("ChatScrollView")
                .onAppear {
                    chatViewModel.checkChats()
                }
            }.onChange(of: chatViewModel.messages) { _ in
                withAnimation {
                    scrollView.scrollTo("ChatScrollView", anchor: .bottom)
                }
            }
            .padding()
        }
        HStack {
            TextField("Type a message", text: $chatViewModel.content)
            Button("Send") {
                chatViewModel.sendMessage()
            }
        }
        .padding()
    }
}

//struct ChatView_Previews: PreviewProvider {
//    static var previews: some View {
//        let testChat = Chats(name: "", id: "")
//        return ChatView(chat: testChat)
//    }
//}
