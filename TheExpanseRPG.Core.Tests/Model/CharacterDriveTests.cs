using FluentAssertions;
using TheExpanseRPG.Core.Model;

namespace TheExpanseRPG.Core.Tests.Model;

public class CharacterDriveTests
{
    readonly string driveName = "drivename";
    readonly string driveDescription = "drivedescription";
    readonly string driveQualityDescription = "drivequalitydescription";
    readonly string driveDownfallDescription = "drivedownfalldescription";
    readonly string driveQuality = "drivequality";
    readonly string driveDownfall = "drivedownfall";
    readonly CharacterDrive _characterDrive;

    public CharacterDriveTests()
    {
        _characterDrive = new(
                driveName,
                driveDescription,
                driveQualityDescription,
                driveDownfallDescription,
                driveQuality,
                driveDownfall
                );
    }
    [Fact]
    public void Constructor_DriveNameIsSet()
    {
        _characterDrive.DriveName.Should().Be(driveName);
    }
    [Fact]
    public void Constructor_DriveDescriptionIsSet()
    {
        _characterDrive.DriveDescription.Should().Be(driveDescription);
    }
    [Fact]
    public void Constructor_QualityDescriptionIsSet()
    {
        _characterDrive.QualityDescription.Should().Be(driveQualityDescription);
    }
    [Fact]
    public void Constructor_DownfallDescriptionIsSet()
    {
        _characterDrive.DownfallDescription.Should().Be(driveDownfallDescription);
    }
    [Fact]
    public void Constructor_QualityIsSet()
    {
        _characterDrive.Quality.Should().Be(driveQuality);
    }
    [Fact]
    public void Constructor_DownfallIsSet()
    {
        _characterDrive.Downfall.Should().Be(driveDownfall);
    }
}

