use std::fs;
use std::io::Error as IOError;
use std::path::PathBuf;

use humansize::{format_size, DECIMAL};
use unrar::Archive;

pub struct RarUnzipper {
  test_work_dir: PathBuf,
}
impl RarUnzipper {
  pub fn new(test_dir: &str) -> Self {
    Self {
      test_work_dir: PathBuf::from(test_dir),
    }
  }

  pub fn listing(&self, archive: &PathBuf) {
    let archive_path = self.test_work_dir.join(archive);
    if !archive_path.try_exists().unwrap() {
      println!("{0} is not exist.", archive_path.to_string_lossy());
      return;
    }
    for entry in Archive::new(&archive_path)
      .open_for_listing_split()
      .unwrap()
    {
      println!("{}", entry.unwrap().filename.to_string_lossy());
    }
    /*
    listing @#unzip */
  }
  pub fn unzip(
    &self,
    archive_file_name: &PathBuf,
  ) -> Result<(), Box<dyn std::error::Error>> {
    let archive_path = self.test_work_dir.join(archive_file_name);
    if !archive_path.try_exists().unwrap() {
      println!("{0} is not exist.", archive_path.to_string_lossy());
      return Err(Box::new(IOError::new(
        std::io::ErrorKind::NotFound,
        format!("File not found: {}", archive_path.to_string_lossy()),
      )));
    }

    // create extract dir
    let extract_dir = self
      .test_work_dir
      .join(archive_file_name.as_path().file_stem().unwrap());
    fs::create_dir(&extract_dir).expect(
      format!("Cannot create {} directory", extract_dir.to_string_lossy()).as_str(),
    );

    let mut archive = Archive::new(&archive_path).open_for_processing().unwrap();
    while let Some(header) = archive.read_header()? {
      let entry = header.entry();
      let size = entry.unpacked_size;
      let size_str = format_size(size, DECIMAL);
      if entry.unpacked_size > 0 {
        println!("{}: {}", size_str, entry.filename.to_string_lossy());
      }
      archive = if entry.is_file() {
        header.extract_with_base(extract_dir.clone())?
      } else {
        header.skip()?
      };
      /*
      unzip @#unzip */
    }
    Ok(())
  }
}

#[cfg(test)]
pub mod tests1 {
  use super::*;
  use std::path::Path;

  #[test]
  fn test1() {
    // listing test
    let unzipper = RarUnzipper::new(r#"S:\s\download_samples"#);
    let test_archive = PathBuf::from("RJ335944.zip");
    let _ = unzipper.listing(&test_archive);
  }

  #[test]
  fn test2() {
    // create folder test
    let test_dir = Path::new(r#"S:\s\download_samples\test"#);
    let archive_name = PathBuf::from("RJ01182655.part1.exe");
    let extract_dir = archive_name.as_path().file_stem().unwrap();
    let extract_pull_path = test_dir.join(extract_dir);
    fs::create_dir(&extract_pull_path).expect(
      format!(
        "Cannot create {} directory",
        extract_pull_path.to_string_lossy()
      )
      .as_str(),
    );
  }
}
