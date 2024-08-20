use std::{env, process, path::PathBuf};

use rar_unzipper::RarUnzipper;

mod rar_unzipper;

fn main() {
  println!("Unzipper start");
  let args: Vec<String> = env::args().collect();
  let unzipper = RarUnzipper::new(r#"S:\s\download_samples"#);
  //test_dir @#

  let mut archive_name: PathBuf = PathBuf::new();
  if args.len() > 1 {
    archive_name = PathBuf::from(args[1].clone());
  } else if args.len() == 0 {
    eprintln!("Error: Insufficient arguments provided.");
    process::exit(-1);
  }
  unzipper.listing(&archive_name);
  println!("\nunzip..");
  unzipper
    .unzip(&archive_name)
    .unwrap();
  /*
  main @# */
}
