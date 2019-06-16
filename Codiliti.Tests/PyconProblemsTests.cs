using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codility.Solvers;
using FluentAssertions;
using Xunit;

namespace Codility.Tests
{
    public class PyconProblemsTests
    {
        [Fact]
        public void GetFrequency_Sample1_Expected()
        {
            var input = @"Proin libero. Vitae congue eu consequat ac felis donec. Feugiat.";
            var expected = @"a4b1c4d1e8f1g2i5l2n4o5q1r2s2t3u4";//they have error near f2 and p1 and v1

            var result = PyconProblems.GetFrequency(input);

            result.Should().Be(expected);
        }

        [Fact]
        public void GetFrequency_Sample2_Expected()
        {
            var input = @"Aliquet enim. Id ornare arcu odio ut sem nulla pharetra. Nunc faucibus a pellentesque sit amet porttitor eget dolor morbi. Blandit turpis cursus in hac habitasse platea dictumst quisque. Enim facilisis gravida neque convallis a cras semper. Id cursus metus aliquam eleifend mi in nulla posuere sollicitudin. Mattis enim ut tellus elementum sagittis vitae et leo duis. Lobortis mattis aliquam faucibus purus in. Congue mauris rhoncus aenean vel. Nascetur ridiculus mus mauris vitae ultricies leo integer. Ac turpis egestas maecenas pharetra convallis. Rutrum tellus pellentesque eu tincidunt. Hendrerit dolor magna eget est lorem ipsum dolor sit. Rhoncus dolor purus non enim praesent elementum. Sit amet mattis vulputate enim nulla aliquet porttitor lacus luctus. Pharetra pharetra massa massa ultricies mi. Nunc mi ipsum faucibus vitae aliquet nec ullamcorper. Ipsum nunc aliquet bibendum enim. Habitant morbi tristique senectus et netus et. Metus dictum at tempor commodo ullamcorper a lacus vestibulum. Eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis. In nulla posuere sollicitudin aliquam ultrices sagittis orci. Laoreet non curabitur gravida arcu ac. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla aliquet. Facilisis gravida neque convallis a cras semper. Cursus turpis massa tincidunt dui ut ornare lectus.";
            var expected = @"a4b1c4d1e8f2g2i5l2n4o5q1r2s2t3u4";//they have error near f2

            var result = PyconProblems.GetFrequency(input);

            result.Should().Be(expected);
        }
        [Fact]
        public void GetFrequency_Sample3_Expected()
        {
            var input = @"Felis donec. Feugiat scelerisque varius morbi enim nunc faucibus a pellentesque sit. Vulputate sapien nec sagittis aliquam malesuada bibendum arcu vitae elementum. Purus sit amet volutpat consequat. Viverra mauris in aliquam sem fringilla ut morbi tincidunt. Felis bibendum ut tristique et egestas. Purus faucibus ornare suspendisse sed nisi lacus. Tincidunt ornare massa eget egestas purus viverra accumsan in nisl. Non tellus orci ac auctor augue. Arcu risus quis varius quam quisque. Porttitor massa id neque aliquam vestibulum morbi blandit. Lectus vestibulum mattis ullamcorper velit sed. Feugiat nisl pretium fusce id velit. Et tortor consequat id porta. Ac auctor augue mauris augue neque gravida. Erat nam at lectus urna duis convallis convallis tellus. Pellentesque id nibh tortor id aliquet lectus proin. Velit ut tortor pretium viverra suspendisse. Risus sed vulputate odio ut. Vitae tortor condimentum lacinia quis. Feugiat nisl pretium fusce id velit ut tortor. Vel orci porta non pulvinar neque laoreet suspendisse. Euismod nisi porta lorem mollis aliquam. Urna cursus eget nunc scelerisque viverra mauris. Quis eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis. Duis at tellus at.";
            var expected = @"a4b1c4d1e8f2g2i5l2n4o5q1r2s2t3u4";//they have error near f2

            var result = PyconProblems.GetFrequency(input);

            result.Should().Be(expected);
        }
        [Fact]
        public void GetFrequency_Sample4_Expected()
        {
            var input = @"Aliquet nec ullamcorper. Ipsum nunc aliquet bibendum enim. Habitant morbi tristique senectus et netus et. Metus dictum at tempor commodo ullamcorper a lacus vestibulum. Eleifend quam adipiscing vitae proin sagittis nisl rhoncus mattis. In nulla posuere sollicitudin aliquam ultrices sagittis orci. Laoreet non curabitur gravida arcu ac. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla aliquet. Facilisis gravida neque convallis a cras semper. Cursus turpis massa tincidunt dui ut ornare lectus sit. Proin libero nunc consequat interdum varius sit. Quis vel eros donec ac odio. Ut enim blandit volutpat maecenas volutpat. Diam quam nulla porttitor massa id neque aliquam. Aliquam etiam erat velit scelerisque in. Etiam non quam lacus suspendisse faucibus interdum posuere lorem ipsum. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Vulputate odio ut enim blandit volutpat maecenas volutpat blandit aliquam. Duis at tellus at urna condimentum. Placerat.";
            var expected = @"a4b1c4d1e8f2g2i5l2n4o5q1r2s2t3u4";//they have error near f2

            var result = PyconProblems.GetFrequency(input);

            result.Should().Be(expected);
        }


        [Fact]
        public void Zip_Sample1_Ok()
        {
            var input = @"aaabccccCCaB";
            var expected = @"3ab4c2CaB";

            var result = PyconProblems.Zip(input);

            result.Should().Be(expected);
        }
        [Fact]
        public void Zip_Sample2_Ok()
        {
            var input = @"aaaaaaaaaaabbbbbbbbbbbbbbcddddeeeeeeeeeeeeefffggggggggggghhhhhhiiijjjjjjjjjj";
            var expected = @"3ab4c2CaB";

            var result = PyconProblems.Zip(input);

            result.Should().Be(expected);
        }
        [Fact]
        public void Zip_Sample3_Ok()
        {
            var input = @"aaaaaaaaaabcccccccccccccccdddeeeeeeeeeeeeeeeffffffffggggggggghhhhhhhiijjjjj";
            var expected = @"3ab4c2CaB";

            var result = PyconProblems.Zip(input);

            result.Should().Be(expected);
        }
        [Fact]
        public void Zip_Sample4_Ok()
        {
            var input = @"aaaaaaaaabbbbbccccccccddddddddddeeeeeeeeeeeefffffffffffffffgggghhhhhhhhhhhhhiiiiiiiiijjjjjjjj";
            var expected = @"3ab4c2CaB";

            var result = PyconProblems.Zip(input);

            result.Should().Be(expected);
        }

        [Fact]
        public void GenerateKollatz_17_Ok()
        {
            var sequence = PyconProblems.GenerateKollatz(17).ToArray();
            var seq = sequence.Select(n => $"{n}");
            var strSeq = string.Join(" ", seq);

            strSeq.Should().Be("17 52 26 13 40 20 10 5 16 8 4 2 1");
        }
        [Fact]
        public void GenerateKollatz_45_Ok()
        {
            var sequence = PyconProblems.GenerateKollatz(45).ToArray();
            var seq = sequence.Select(n => $"{n}");
            var strSeq = string.Join(" ", seq);

            strSeq.Should().Be("17 52 26 13 40 20 10 5 16 8 4 2 1");
        }
        [Fact]
        public void GenerateKollatz_96_Ok()
        {
            var sequence = PyconProblems.GenerateKollatz(96).ToArray();
            var seq = sequence.Select(n => $"{n}");
            var strSeq = string.Join(" ", seq);

            strSeq.Should().Be("17 52 26 13 40 20 10 5 16 8 4 2 1");
        }
        [Fact]
        public void GenerateKollatz_11_Ok()
        {
            var sequence = PyconProblems.GenerateKollatz(11).ToArray();
            var seq = sequence.Select(n => $"{n}");
            var strSeq = string.Join(" ", seq);

            strSeq.Should().Be("17 52 26 13 40 20 10 5 16 8 4 2 1");
        }

        [Fact]
        public void Cesar_Sample_Ok()
        {
            string raw = @"sodales ut eu tortor aliquam nulla facilisi cras pellentesque sit";
            string part = @"vkjpkmsvsb";
            var expected = 10;

            var result = PyconProblems.Cesar(raw, part);

            result.Should().Be(expected);
        }
        [Fact]
        public void Cesar_Sample1_Ok()
        {
            string raw = @"massa tincidunt dui ut ornare lectus sit proin libero nunc consequat interdum varius sit quis vel eros donec ac odio ut enim blandit volutpat maecenas volutpat diam quam nulla porttitor massa id neque aliquam aliquam etiam erat velit scelerisque in etiam non quam lacus suspendisse faucibus interdum posuere lorem ipsum";
            string part = @"lmol pu lfelqzuylnxmzpuelg xfeamelymqoqzmdlg xfeamelpumylbfmylz";
            var expected = 10;

            var result = PyconProblems.Cesar(raw, part);

            result.Should().Be(expected);
        }
        [Fact]
        public void Cesar_Sample2_Ok()
        {
            string raw = @"arcu risus quis varius quam quisque id nunc congue nisi vitae suscipit tellus mauris a diam maecenas viverra suspendisse potenti nullam ac tortor vitae purus faucibus ornare purus faucibus ornare suspendisse sed nisi lacus sed phasellus vestibulum lorem sed risus ultricies tristique nulla aliquet enim id ornare arcu odio ut";
            string part = @"ornare suspendisse sed nisi lacus sed phasellus vestibulum lorem";
            var expected = 10;

            var result = PyconProblems.Cesar(raw, part);

            result.Should().Be(expected);
        }
        [Fact]
        public void Cesar_Sample3_Ok()
        {
            string raw = @"lectus sit amet aenean vel elit scelerisque mauris pellentesque pulvinar rhoncus mattis rhoncus urna neque viverra justo nec ultrices dui pulvinar sapien et ligula ullamcorper malesuada est lorem ipsum dolor sit amet consectetur adipiscing dolor sed viverra ipsum nunc aliquet bibendum enim facilisis diam maecenas ultricies mi eget mauris pharetra";
            string part = @"gsghenorwcwfqwaunrbzbenfsrniwiseeonwcfh nahaqnozwdhsgnpwpsarh nsaw ntoqwzwfwfnrwo n osq";
            var expected = 10;

            var result = PyconProblems.Cesar(raw, part);

            result.Should().Be(expected);
        }

        [Fact]
        public void Hanoi_2_Ok()
        {
            var solution = PyconProblems.Hanoi(2);
            solution.Should().Be("1-2,1-3,2-3");
        }
        [Fact]
        public void Hanoi_6_Ok()
        {
            var solution = PyconProblems.Hanoi(6);
            solution.Should().Be("1-2,1-3,2-3");
        }
        [Fact]
        public void Hanoi_5_Ok()
        {
            var solution = PyconProblems.Hanoi(5);
            solution.Should().Be("1-2,1-3,2-3");
        }
    }
}
