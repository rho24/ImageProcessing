using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageProcessing.Core
{
    public class BitmapStream : Stream
    {
        private readonly RememberingEnumerable<byte> _bitmapBytes;
        private readonly IFrame<ArgbPixel> _frame;
        private int? _currentCount;
        private long _position = 0;

        public override bool CanRead {
            get { return !_currentCount.HasValue || _currentCount != 0; }
        }

        public override bool CanSeek {
            get { return false; }
        }

        public override bool CanWrite {
            get { return false; }
        }

        public override long Length {
            get { throw new NotImplementedException(); }
        }

        public override long Position {
            get { return _position; }
            set { throw new NotImplementedException(); }
        }

        public BitmapStream(IFrame<ArgbPixel> frame) {
            _frame = frame;
            _bitmapBytes = new RememberingEnumerable<byte>(ToBytes());
        }

        private IEnumerable<byte> ToBytes() {
            //https://en.wikipedia.org/wiki/BMP_file_format#Example_2

            //MP Header

            yield return 0x42;
            yield return 0x4d;

            foreach (var b in BitConverter.GetBytes(LengthBytes())) yield return b;
            
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x7a;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            //DIB Header

            yield return 0x6c;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            foreach (var b in BitConverter.GetBytes(_frame.Width)) yield return b;
            foreach (var b in BitConverter.GetBytes(_frame.Height)) yield return b;

            yield return 0x01;
            yield return 0x00;

            yield return 0x20;
            yield return 0x00;

            yield return 0x03;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x20;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x13;
            yield return 0x0b;
            yield return 0x00;
            yield return 0x00;

            yield return 0x13;
            yield return 0x0b;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0x00;
            yield return 0xff;
            yield return 0x00;

            yield return 0x00;
            yield return 0xff;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0xff;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0xff;

            yield return 0x20;
            yield return 0x6e;
            yield return 0x69;
            yield return 0x57;

            yield return 0x24;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            yield return 0x00;
            yield return 0x00;
            yield return 0x00;
            yield return 0x00;

            //Pixels

            foreach (var row in _frame.Data.Reverse()) {
                foreach (var pixel in row) {
                    yield return pixel.B;
                    yield return pixel.G;
                    yield return pixel.R;
                    yield return pixel.A;
                }
            }
        }

        private int LengthBytes() {
            return 122 + (4*_frame.Height*_frame.Width);
        }

        public override void Flush() {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException();
        }

        public override void SetLength(long value) {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count) {
            var bytes = _bitmapBytes.Select(b => {
                _position++;
                return b;
            }).Take(count).ToList();

            bytes.CopyTo(buffer);
            _currentCount = bytes.Count;

            if(_currentCount == 0)
                _bitmapBytes.FinallyDispose();

            return _currentCount.Value;
        }

        public override void Write(byte[] buffer, int offset, int count) {
            throw new NotImplementedException();
        }
    }
}