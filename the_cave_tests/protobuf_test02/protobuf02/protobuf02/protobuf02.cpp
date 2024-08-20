// protobuf02.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <fstream>
#include "person.pb.h"

void make_test_bin_data_file() {
  Person person;
  person.set_name("Alice");
  person.set_id(12345);
  person.add_email("alice@example.com");
  person.add_email("alice2@example.com");

  std::fstream output("person.bin", std::ios::out | std::ios::trunc | std::ios::binary);
  person.SerializeToOstream(&output);
}

int main()
{
  std::cout << "program start\n";
  // 읽어올 데이터 파일
  std::fstream input("person.bin", std::ios::in | std::ios::binary);

  // Person 객체 생성
  Person person;

  // 파일에서 데이터 읽기
  if (!person.ParseFromIstream(&input)) {
    std::cerr << "Failed to parse person data." << std::endl;
    return -1;
  }

  // 읽은 데이터 출력
  std::cout << "Name: " << person.name() << std::endl;
  std::cout << "ID: " << person.id() << std::endl;
  for (const std::string& email : person.email()) {
    std::cout << "Email: " << email << std::endl;
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
