﻿namespace RedBlueGames.Tools.Tests
{
    using System.Collections;
    using UnityEngine;
    using NUnit.Framework;

    public class BulkRenamerUnitTests
    {
        [Test]
        public void SearchString_Empty_DoesNothing()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            bulkRenamer.SearchString = string.Empty;
            var name = "ThisIsAName";
            var originalInput = name;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(originalInput, result);
        }

        [Test]
        public void SearchString_OneMatch_IsReplaced()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            bulkRenamer.SearchString = "Hero";
            bulkRenamer.ReplacementString = "A";
            var name = "CHAR_Hero_Spawn";
            var expected = "CHAR_A_Spawn";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchString_MultipleMatches_AllAreReplaced()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "StoolDoodad";
            bulkRenamer.SearchString = "o";
            var expected = "StlDdad";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchString_PartialMatches_AreNotReplaced()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero_Spawn";
            bulkRenamer.SearchString = "Heroine";
            var expected = name;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchString_Replacement_SubstitutesForSearchString()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero_Spawn";
            bulkRenamer.SearchString = "Hero";
            bulkRenamer.ReplacementString = "Link";
            var expected = "Char_Link_Spawn";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchString_DoesNotMatchCase_Replaces()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            bulkRenamer.SearchIsCaseSensitive = false;
            bulkRenamer.SearchString = "ZelDa";
            bulkRenamer.ReplacementString = "blah";
            var objectName = "ZELDAzelda";
            var expected = "blahblah";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, objectName)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchStringCaseSensitive_DoesNotMatchCase_DoesNotReplace()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            bulkRenamer.SearchIsCaseSensitive = true;
            bulkRenamer.SearchString = "zelda";
            bulkRenamer.ReplacementString = "blah";
            var objectName = "ZELDA";
            var expected = "ZELDA";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, objectName)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchStringCaseSensitive_MatchesCase_Replaces()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            bulkRenamer.SearchIsCaseSensitive = true;
            bulkRenamer.SearchString = "ZeldA";
            bulkRenamer.ReplacementString = "blah";
            var objectName = "ZeldA";
            var expected = "blah";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, objectName)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddPrefix_Empty_DoesNothing()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero_Spawn";
            bulkRenamer.Prefix = string.Empty;
            var expected = name;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddPrefix_ValidPrefix_IsAdded()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Hero_Spawn";
            bulkRenamer.Prefix = "Char_";
            var expected = "Char_Hero_Spawn";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddSuffix_Empty_DoesNothing()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero_Spawn";
            bulkRenamer.Prefix = string.Empty;
            var expected = name;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddSuffix_ValidPrefix_IsAdded()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.Suffix = "_Spawn";
            var expected = "Char_Hero_Spawn";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Trimming_DeleteNone_IsUnchanged()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.NumFrontDeleteChars = 0;
            bulkRenamer.NumBackDeleteChars = 0;
            var expected = name;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Trimming_DeleteOneFromFront_IsDeleted()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.NumFrontDeleteChars = 1;
            bulkRenamer.NumBackDeleteChars = 0;
            var expected = "har_Hero";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Trimming_DeleteOneFromBack_IsDeleted()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.NumFrontDeleteChars = 0;
            bulkRenamer.NumBackDeleteChars = 1;
            var expected = "Char_Her";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Trimming_DeleteFromFrontAndBack_IsDeleted()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.NumFrontDeleteChars = 1;
            bulkRenamer.NumBackDeleteChars = 1;
            var expected = "har_Her";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Trimming_DeleteLongerThanString_EntireStringIsDeleted()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.NumFrontDeleteChars = name.Length + 2;
            bulkRenamer.NumBackDeleteChars = 0;
            var expected = string.Empty;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Trimming_DeleteTooManyFromBothDirections_EntireStringIsDeleted()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.NumFrontDeleteChars = name.Length;
            bulkRenamer.NumBackDeleteChars = name.Length;
            var expected = string.Empty;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Trimming_DeleteNegative_IsUnchanged()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.NumFrontDeleteChars = -1;
            bulkRenamer.NumBackDeleteChars = -10;
            var expected = name;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Enumerating_NoFormat_DoesNothing()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.CountFormat = string.Empty;
            var expected = name;

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Enumerating_SingleDigitFormat_AddsCount()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.CountFormat = "0";
            bulkRenamer.StartingCount = 0;
            var expected = "Char_Hero0";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Enumerating_CountSeveralTimes_CountsUp()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var names = new string[]
            {
                "BlockA",
                "BlockB",
                "BlockC",
                "BlockD",
                "BlockE",
            };
            bulkRenamer.CountFormat = "0";
            bulkRenamer.StartingCount = 1;
            var expectedNames = new string[]
            {
                "BlockA1",
                "BlockB2",
                "BlockC3",
                "BlockD4",
                "BlockE5",
            };

            // Act
            var results = bulkRenamer.GetRenamedStrings(false, names);

            // Assert
            Assert.AreEqual(
                expectedNames.Length,
                results.Length,
                "Expected Results and results should have the same number of entries but didn't.");
            for (int i = 0; i < results.Length; ++i)
            {
                var expected = expectedNames[i];
                Assert.AreEqual(expected, results[i]);
            }
        }

        [Test]
        public void Enumerating_StartFromNonZero_AddsCorrectCount()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.CountFormat = "0";
            bulkRenamer.StartingCount = -1;
            var expected = "Char_Hero-1";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Enumerating_InvalidFormat_IsIgnored()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero";
            bulkRenamer.CountFormat = "s";
            bulkRenamer.StartingCount = 100;
            var expected = "Char_Hero";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AllOperations_ValidOperations_RenamesCorrectly()
        {
            // Arrange
            var bulkRenamer = new BulkRenamer();
            var name = "Char_Hero_Idle";
            bulkRenamer.SearchString = "r_H";
            bulkRenamer.ReplacementString = "t_Z";
            bulkRenamer.NumFrontDeleteChars = 1;
            bulkRenamer.NumBackDeleteChars = 5;
            bulkRenamer.Prefix = "a_";
            bulkRenamer.Suffix = "AA";
            bulkRenamer.CountFormat = "D4";
            bulkRenamer.StartingCount = 100;
            var expected = "a_hat_ZeroAA0100";

            // Act
            string result = bulkRenamer.GetRenamedStrings(false, name)[0];

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}