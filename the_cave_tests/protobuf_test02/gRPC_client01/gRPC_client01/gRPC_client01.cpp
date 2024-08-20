// gRPC_client01.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <string>
#include <grpcpp/grpcpp.h>
#include "messenger.grpc.pb.h"

using grpc::Channel;
using grpc::ClientContext;
using grpc::Status;
using messenger::Messenger;
using messenger::Message;
using messenger::MessageResponse;

int main() {
    std::string addr = "localhost:50051"; // 서버 주소
    Messenger::Stub stub(grpc::CreateChannel(
        addr, grpc::InsecureChannelCredentials()));

    // 보낼 메시지 입력
    std::string message_text;
    std::cout << "Enter message to send: ";
    std::getline(std::cin, message_text);

    // gRPC 요청 생성 및 전송
    Message request;
    request.set_text(message_text);
    MessageResponse response;
    ClientContext context;

    Status status = stub.SendMessage(&context, request, &response);

    // 응답 처리
    if (status.ok()) {
        std::cout << "Server response: " << response.response() << std::endl;
    } else {
        std::cout << "RPC failed: " << status.error_code() << ": "
                  << status.error_message() << std::endl;
    }

    return 0;
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
