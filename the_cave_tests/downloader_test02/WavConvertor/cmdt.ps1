$input_wav = "S:\s\voice\RJ320480 SSS+\Track0.JKレイプ.wav"
$output = "S:\s\voice\RJ320480 SSS+\Track0.JKレイプ_out.mp3"
if (-not (Test-Path "S:\")) {
  "Mount S disk first."
} else {
  ffmpeg -i "$input_wav" -b:a 192k -q:a 2 -ar 44100 -ac 2 -acodec libmp3lame `
    -vn -map_metadata 0 -id3v2_version 3 "$output"
}