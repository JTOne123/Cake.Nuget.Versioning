using System;
using Xunit;

namespace Cake.Nuget.Versioning.Test
{
    public class NugetVersioningTest
    {
        [Fact]
        public void Test1()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "refs/heads/master");
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void Test2()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "master");
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void Test3()
        {
            var settings = new BuildNugetVersionFromBranchSettings { BranchName = "refs/heads/release/v10", PreReleaseFilters = new[] { "^master$", "^release/" } };
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, settings);
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void Test4()
        {
            var settings = new BuildNugetVersionFromBranchSettings { BranchName = "release/v10", PreReleaseFilters = new[] { "^master$", "^release/" } };
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, settings);
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void Test5()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "refs/heads/feature/test_cake_version");
            Assert.Equal("1.0.0-feature-test-cake-v", version);
        }

        [Fact]
        public void Test6()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "feature/test_cake_version");
            Assert.Equal("1.0.0-feature-test-cake-v", version);
        }

        [Fact]
        public void Test7()
        {
            var settings = new BuildNugetVersionFromBranchSettings { BranchName = "refs/heads/feature/test_cake_version", TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, settings);
            Assert.Equal("1.0.0-test-cake-version", version);
        }

        [Fact]
        public void Test8()
        {
            var settings = new BuildNugetVersionFromBranchSettings { BranchName = "refs/heads/feature/test_cake_version", TrimPatterns = new[] { "feature/" }, AlwaysApplyBranchPrefix = true };
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, settings);
            Assert.Equal("1.0.0-b-test-cake-version", version);
        }

        [Fact]
        public void Test9()
        {
            var settings = new BuildNugetVersionFromBranchSettings { BranchName = "refs/heads/feature/test_cake_version", FilterGitReferences = false };
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, settings);
            Assert.Equal("1.0.0-refs-heads-feature", version);
        }

        [Fact]
        public void Test10()
        {
            var settings = new BuildNugetVersionFromBranchSettings { BranchName = "refs/heads/feature/test_cake_version", FilterGitReferences = false, TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, settings);
            Assert.Equal("1.0.0-refs-heads-test-cak", version);
        }

        [Fact]
        public void Test11()
        {
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, "refs/heads/master", "abcdabcd");
            Assert.Equal("1.0.0+abcdabcd", version);
        }

        [Fact]
        public void Test12()
        {
            var settings = new BuildNugetVersionFromBranchSemVer200Settings { BranchName = "refs/heads/feature/test_cake_version", Hash = "abcdabcd", TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, settings);
            Assert.Equal("1.0.0-test-cake-version.abcdabcd", version);
        }

        [Fact]
        public void Test13()
        {
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, "refs/heads/master");
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void Test14()
        {
            var settings = new BuildNugetVersionFromBranchSemVer200Settings { BranchName = "refs/heads/feature/test_cake_version", TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, settings);
            Assert.Equal("1.0.0-test-cake-version", version);
        }

        [Fact]
        public void Test15()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "feature/release_cake_version");
            Assert.Equal("1.0.0-feature-release-cak", version);
        }

        [Fact]
        public void Test16()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "feature/master_cake_version");
            Assert.Equal("1.0.0-feature-master-cake", version);
        }

        [Fact]
        public void Test17()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "refs/heads/feature/release_cake_version");
            Assert.Equal("1.0.0-feature-release-cak", version);
        }

        [Fact]
        public void Test18()
        {
            string version = new TestContext().BuildNugetVersionFromBranch(1, 0, 0, "refs/heads/feature/master_cake_version");
            Assert.Equal("1.0.0-feature-master-cake", version);
        }

        [Fact]
        public void Test19()
        {
            var settings = new BuildNugetVersionFromBranchSemVer200Settings { BranchName = "refs/heads/feature/test_cake_version", Hash = "abcdabcd", BranchChangeNumber = 132, TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, settings);
            Assert.Equal("1.0.0-test-cake-version.132.abcdabcd", version);
        }

        [Fact]
        public void Test20()
        {
            string longbranchname = new string('a', 255);
            var settings = new BuildNugetVersionFromBranchSemVer200Settings { BranchName = $"refs/heads/feature/{longbranchname}", Hash = "abcdabcdabcdabcdabcdabcdabcdabcdabcdabcd", BranchChangeNumber = 132, TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, settings);
            string shortenedbranchname = new string('a', 200);
            Assert.Equal($"1.0.0-{shortenedbranchname}.132.abcdabcdabcdabcdabcdabcdabcdabcdabcdabcd", version);
        }

        [Fact]
        public void Test21()
        {
            var settings = new BuildNugetVersionFromBranchSemVer200Settings { BranchName = "refs/heads/feature/test_cake_version+something+else", Hash = "abcdabcd", TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, settings);
            Assert.Equal("1.0.0-test-cake-version-something-else.abcdabcd", version);
        }

        [Fact]
        public void Test22()
        {
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 0, "refs/heads/master");
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void Test23()
        {
            var settings = new BuildNugetVersionFromBranchSemVer200Settings { PreReleaseFilters = new string[]{ "^master$", "^releases/release-.*" }, BranchName = "refs/heads/releases/release-v4_2", BranchChangeNumber = 5, TrimPatterns = new[] { "feature/" } };
            string version = new TestContext().BuildNugetVersionFromBranchSemVer200(1, 0, 3, settings);
            Assert.Equal("1.0.8", version);
        }
    }
}