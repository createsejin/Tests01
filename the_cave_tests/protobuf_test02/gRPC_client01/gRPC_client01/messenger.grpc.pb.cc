// Generated by the gRPC C++ plugin.
// If you make any local change, they will be lost.
// source: messenger.proto

#include "messenger.pb.h"
#include "messenger.grpc.pb.h"

#include <functional>
#include <grpcpp/support/async_stream.h>
#include <grpcpp/support/async_unary_call.h>
#include <grpcpp/impl/channel_interface.h>
#include <grpcpp/impl/client_unary_call.h>
#include <grpcpp/support/client_callback.h>
#include <grpcpp/support/message_allocator.h>
#include <grpcpp/support/method_handler.h>
#include <grpcpp/impl/rpc_service_method.h>
#include <grpcpp/support/server_callback.h>
#include <grpcpp/impl/codegen/server_callback_handlers.h>
#include <grpcpp/server_context.h>
#include <grpcpp/impl/service_type.h>
#include <grpcpp/support/sync_stream.h>
namespace messenger {

static const char* Messenger_method_names[] = {
  "/messenger.Messenger/SendMessage",
};

std::unique_ptr< Messenger::Stub> Messenger::NewStub(const std::shared_ptr< ::grpc::ChannelInterface>& channel, const ::grpc::StubOptions& options) {
  (void)options;
  std::unique_ptr< Messenger::Stub> stub(new Messenger::Stub(channel, options));
  return stub;
}

Messenger::Stub::Stub(const std::shared_ptr< ::grpc::ChannelInterface>& channel, const ::grpc::StubOptions& options)
  : channel_(channel), rpcmethod_SendMessage_(Messenger_method_names[0], options.suffix_for_stats(),::grpc::internal::RpcMethod::NORMAL_RPC, channel)
  {}

::grpc::Status Messenger::Stub::SendMessage(::grpc::ClientContext* context, const ::messenger::Message& request, ::messenger::MessageResponse* response) {
  return ::grpc::internal::BlockingUnaryCall< ::messenger::Message, ::messenger::MessageResponse, ::grpc::protobuf::MessageLite, ::grpc::protobuf::MessageLite>(channel_.get(), rpcmethod_SendMessage_, context, request, response);
}

void Messenger::Stub::async::SendMessage(::grpc::ClientContext* context, const ::messenger::Message* request, ::messenger::MessageResponse* response, std::function<void(::grpc::Status)> f) {
  ::grpc::internal::CallbackUnaryCall< ::messenger::Message, ::messenger::MessageResponse, ::grpc::protobuf::MessageLite, ::grpc::protobuf::MessageLite>(stub_->channel_.get(), stub_->rpcmethod_SendMessage_, context, request, response, std::move(f));
}

void Messenger::Stub::async::SendMessage(::grpc::ClientContext* context, const ::messenger::Message* request, ::messenger::MessageResponse* response, ::grpc::ClientUnaryReactor* reactor) {
  ::grpc::internal::ClientCallbackUnaryFactory::Create< ::grpc::protobuf::MessageLite, ::grpc::protobuf::MessageLite>(stub_->channel_.get(), stub_->rpcmethod_SendMessage_, context, request, response, reactor);
}

::grpc::ClientAsyncResponseReader< ::messenger::MessageResponse>* Messenger::Stub::PrepareAsyncSendMessageRaw(::grpc::ClientContext* context, const ::messenger::Message& request, ::grpc::CompletionQueue* cq) {
  return ::grpc::internal::ClientAsyncResponseReaderHelper::Create< ::messenger::MessageResponse, ::messenger::Message, ::grpc::protobuf::MessageLite, ::grpc::protobuf::MessageLite>(channel_.get(), cq, rpcmethod_SendMessage_, context, request);
}

::grpc::ClientAsyncResponseReader< ::messenger::MessageResponse>* Messenger::Stub::AsyncSendMessageRaw(::grpc::ClientContext* context, const ::messenger::Message& request, ::grpc::CompletionQueue* cq) {
  auto* result =
    this->PrepareAsyncSendMessageRaw(context, request, cq);
  result->StartCall();
  return result;
}

Messenger::Service::Service() {
  AddMethod(new ::grpc::internal::RpcServiceMethod(
      Messenger_method_names[0],
      ::grpc::internal::RpcMethod::NORMAL_RPC,
      new ::grpc::internal::RpcMethodHandler< Messenger::Service, ::messenger::Message, ::messenger::MessageResponse, ::grpc::protobuf::MessageLite, ::grpc::protobuf::MessageLite>(
          [](Messenger::Service* service,
             ::grpc::ServerContext* ctx,
             const ::messenger::Message* req,
             ::messenger::MessageResponse* resp) {
               return service->SendMessage(ctx, req, resp);
             }, this)));
}

Messenger::Service::~Service() {
}

::grpc::Status Messenger::Service::SendMessage(::grpc::ServerContext* context, const ::messenger::Message* request, ::messenger::MessageResponse* response) {
  (void) context;
  (void) request;
  (void) response;
  return ::grpc::Status(::grpc::StatusCode::UNIMPLEMENTED, "");
}


}  // namespace messenger
