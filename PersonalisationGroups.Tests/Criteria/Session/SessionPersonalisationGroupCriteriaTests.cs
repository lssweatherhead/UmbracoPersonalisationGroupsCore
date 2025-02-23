﻿using System;
using NUnit.Framework;
using Moq;
using Our.Umbraco.PersonalisationGroups.Core.Criteria.Session;
using Our.Umbraco.PersonalisationGroups.Core.Providers.Session;

namespace Our.Umbraco.PersonalisationGroups.Tests.Criteria.Session
{
    [TestFixture]
    public class SessionPersonalisationGroupCriteriaTests
    {
        private const string DefinitionFormat = "{{ \"key\": \"{0}\", \"match\": \"{1}\", \"value\": \"{2}\" }}";

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithEmptyDefinition_ThrowsException()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);

            // Act
            Assert.Throws<ArgumentNullException>(() => criteria.MatchesVisitor(null));
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithInvalidDefinition_ThrowsException()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = "invalid";

            // Act
            Assert.Throws<ArgumentException>(() => criteria.MatchesVisitor(definition));
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionExists_WithExistingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "key", "Exists", string.Empty);

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionExists_WithMissingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "missing-key", "Exists", string.Empty);

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionAbsent_WithAbsentSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "missing-key", "DoesNotExist", string.Empty);

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionAbsent_WithExistingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "key", "DoesNotExist", string.Empty);

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionMatchingValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "key", "MatchesValue", "aaa,bbb,ccc");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionMatchingValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "key", "MatchesValue", "aaa,bbb,xxx");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionContainingValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "key", "ContainsValue", "bbb");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForSessionContainingValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "key", "ContainsValue", "xxx");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }
        
        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForGreaterThanDateValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "dateCompareTest", "GreaterThanValue", "1-APR-2015");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForGreaterThanDateValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "dateCompareTest", "GreaterThanValue", "1-JUN-2015");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForGreaterThanNumericValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "numericCompareTest", "GreaterThanValue", "3");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForGreaterThanNumericValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "numericCompareTest", "GreaterThanValue", "7");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForGreaterThanStringValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "stringCompareTest", "GreaterThanValue", "aaa");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForGreaterThanStringValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "stringCompareTest", "GreaterThanValue", "ccc");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForLessThanDateValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "dateCompareTest", "LessThanValue", "1-JUN-2015");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForLessThanDateValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "dateCompareTest", "LessThanValue", "1-APR-2015");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForLessThanNumericValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "numericCompareTest", "LessThanValue", "7");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForLessThanNumericValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "numericCompareTest", "LessThanValue", "3");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForLessThanStringValue_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "stringCompareTest", "LessThanValue", "ccc");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionForLessThanStringValue_WithNonMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "stringCompareTest", "LessThanValue", "aaa");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionMatchesRegex_WithMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "regexTest", "MatchesRegex", "[a-z]");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionMatchesRegex_WithNonMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "regexTest", "MatchesRegex", "[A-Z]");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionDoesNotMatchRegex_WithMatchingSession_ReturnsFalse()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "regexTest", "DoesNotMatchRegex", "[a-z]");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void SessionPersonalisationGroupCriteria_MatchesVisitor_WithDefinitionDoesNotMatchRegex_WithNonMatchingSession_ReturnsTrue()
        {
            // Arrange
            var mockSessionProvider = MockSessionProvider();
            var criteria = new SessionPersonalisationGroupCriteria(mockSessionProvider.Object);
            var definition = string.Format(DefinitionFormat, "regexTest", "DoesNotMatchRegex", "[A-Z]");

            // Act
            var result = criteria.MatchesVisitor(definition);

            // Assert
            Assert.IsTrue(result);
        }

        private static Mock<ISessionProvider> MockSessionProvider()
        {
            var mock = new Mock<ISessionProvider>();

            mock.Setup(x => x.KeyExists(It.Is<string>(y => y == "key"))).Returns(true);
            mock.Setup(x => x.KeyExists(It.Is<string>(y => y == "dateCompareTest"))).Returns(true);
            mock.Setup(x => x.KeyExists(It.Is<string>(y => y == "numericCompareTest"))).Returns(true);
            mock.Setup(x => x.KeyExists(It.Is<string>(y => y == "stringCompareTest"))).Returns(true);
            mock.Setup(x => x.KeyExists(It.Is<string>(y => y == "missing-key"))).Returns(false);
            mock.Setup(x => x.KeyExists(It.Is<string>(y => y == "regexTest"))).Returns(true);
            mock.Setup(x => x.GetValue(It.Is<string>(y => y == "key"))).Returns("aaa,bbb,ccc");
            mock.Setup(x => x.GetValue(It.Is<string>(y => y == "dateCompareTest"))).Returns("1-MAY-2015 10:30:00");
            mock.Setup(x => x.GetValue(It.Is<string>(y => y == "numericCompareTest"))).Returns("5");
            mock.Setup(x => x.GetValue(It.Is<string>(y => y == "stringCompareTest"))).Returns("bbb");
            mock.Setup(x => x.GetValue(It.Is<string>(y => y == "regexTest"))).Returns("b");

            return mock;
        }
    }
}
