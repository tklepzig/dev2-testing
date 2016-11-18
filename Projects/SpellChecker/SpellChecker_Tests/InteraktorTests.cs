﻿using FluentAssertions;
using SpellChecker.Resourcen;
using SpellChecker.UI;
using Xunit;

namespace SpellChecker.Test
{
    public class InteraktorTests
    {
        [Fact]
        public void Prüfen_gibt_fehlerhafte_Wörter_korrekt_zurück()
        {
            // Arrange
            var text = "Ich bin ein Baum\r\n" +
                       "grüner Baum";

            // Act
            var fehlerhafteWörter = new Interaktor(new TextInWörterZerleger(), new WörterbuchProvider(new DateiLeser(), new WörterErmittler()), new TextAnalyse(), new WortAufbereiter()).Prüfen(text);

            // Assert
            fehlerhafteWörter.ShouldBeEquivalentTo(new[] { "Baum [1,13;2,8]", "grüner [2,1]" });
        }
    }
}
