using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PodcastApi.Migrations
{
    /// <inheritdoc />
    public partial class FixedDbSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Podcast_PodcastId",
                table: "Episode");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Guest_Episode_EpisodeId",
                table: "Episode2Guest");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Guest_Guest_GuestId",
                table: "Episode2Guest");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Tag_Episode_EpisodeId",
                table: "Episode2Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Tag_Tag_TagId",
                table: "Episode2Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritedEpisode_Episode_EpisodeId",
                table: "FavoritedEpisode");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritedEpisode_User_UserId",
                table: "FavoritedEpisode");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaLink_Guest_GuestId",
                table: "SocialMediaLink");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaLink_Host_HostId",
                table: "SocialMediaLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialMediaLink",
                table: "SocialMediaLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Host",
                table: "Host");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guest",
                table: "Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritedEpisode",
                table: "FavoritedEpisode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode2Tag",
                table: "Episode2Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode2Guest",
                table: "Episode2Guest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode",
                table: "Episode");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "SocialMediaLink",
                newName: "SocialMediaLinks");

            migrationBuilder.RenameTable(
                name: "Host",
                newName: "Hosts");

            migrationBuilder.RenameTable(
                name: "Guest",
                newName: "Guests");

            migrationBuilder.RenameTable(
                name: "FavoritedEpisode",
                newName: "FavoritedEpisodes");

            migrationBuilder.RenameTable(
                name: "Episode2Tag",
                newName: "Episode2Tags");

            migrationBuilder.RenameTable(
                name: "Episode2Guest",
                newName: "Episode2Guests");

            migrationBuilder.RenameTable(
                name: "Episode",
                newName: "Episodes");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMediaLink_HostId",
                table: "SocialMediaLinks",
                newName: "IX_SocialMediaLinks_HostId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMediaLink_GuestId",
                table: "SocialMediaLinks",
                newName: "IX_SocialMediaLinks_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritedEpisode_UserId",
                table: "FavoritedEpisodes",
                newName: "IX_FavoritedEpisodes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Episode2Tag_TagId",
                table: "Episode2Tags",
                newName: "IX_Episode2Tags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Episode2Guest_GuestId",
                table: "Episode2Guests",
                newName: "IX_Episode2Guests_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Episode_PodcastId",
                table: "Episodes",
                newName: "IX_Episodes_PodcastId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialMediaLinks",
                table: "SocialMediaLinks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guests",
                table: "Guests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritedEpisodes",
                table: "FavoritedEpisodes",
                columns: new[] { "EpisodeId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode2Tags",
                table: "Episode2Tags",
                columns: new[] { "EpisodeId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode2Guests",
                table: "Episode2Guests",
                columns: new[] { "EpisodeId", "GuestId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Guests_Episodes_EpisodeId",
                table: "Episode2Guests",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Guests_Guests_GuestId",
                table: "Episode2Guests",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Tags_Episodes_EpisodeId",
                table: "Episode2Tags",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Tags_Tags_TagId",
                table: "Episode2Tags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Podcast_PodcastId",
                table: "Episodes",
                column: "PodcastId",
                principalTable: "Podcast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritedEpisodes_Episodes_EpisodeId",
                table: "FavoritedEpisodes",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritedEpisodes_Users_UserId",
                table: "FavoritedEpisodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaLinks_Guests_GuestId",
                table: "SocialMediaLinks",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaLinks_Hosts_HostId",
                table: "SocialMediaLinks",
                column: "HostId",
                principalTable: "Hosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Guests_Episodes_EpisodeId",
                table: "Episode2Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Guests_Guests_GuestId",
                table: "Episode2Guests");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Tags_Episodes_EpisodeId",
                table: "Episode2Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Episode2Tags_Tags_TagId",
                table: "Episode2Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Podcast_PodcastId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritedEpisodes_Episodes_EpisodeId",
                table: "FavoritedEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritedEpisodes_Users_UserId",
                table: "FavoritedEpisodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaLinks_Guests_GuestId",
                table: "SocialMediaLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialMediaLinks_Hosts_HostId",
                table: "SocialMediaLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialMediaLinks",
                table: "SocialMediaLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hosts",
                table: "Hosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guests",
                table: "Guests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritedEpisodes",
                table: "FavoritedEpisodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episodes",
                table: "Episodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode2Tags",
                table: "Episode2Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode2Guests",
                table: "Episode2Guests");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "SocialMediaLinks",
                newName: "SocialMediaLink");

            migrationBuilder.RenameTable(
                name: "Hosts",
                newName: "Host");

            migrationBuilder.RenameTable(
                name: "Guests",
                newName: "Guest");

            migrationBuilder.RenameTable(
                name: "FavoritedEpisodes",
                newName: "FavoritedEpisode");

            migrationBuilder.RenameTable(
                name: "Episodes",
                newName: "Episode");

            migrationBuilder.RenameTable(
                name: "Episode2Tags",
                newName: "Episode2Tag");

            migrationBuilder.RenameTable(
                name: "Episode2Guests",
                newName: "Episode2Guest");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMediaLinks_HostId",
                table: "SocialMediaLink",
                newName: "IX_SocialMediaLink_HostId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialMediaLinks_GuestId",
                table: "SocialMediaLink",
                newName: "IX_SocialMediaLink_GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritedEpisodes_UserId",
                table: "FavoritedEpisode",
                newName: "IX_FavoritedEpisode_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Episodes_PodcastId",
                table: "Episode",
                newName: "IX_Episode_PodcastId");

            migrationBuilder.RenameIndex(
                name: "IX_Episode2Tags_TagId",
                table: "Episode2Tag",
                newName: "IX_Episode2Tag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_Episode2Guests_GuestId",
                table: "Episode2Guest",
                newName: "IX_Episode2Guest_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialMediaLink",
                table: "SocialMediaLink",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Host",
                table: "Host",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guest",
                table: "Guest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritedEpisode",
                table: "FavoritedEpisode",
                columns: new[] { "EpisodeId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode",
                table: "Episode",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode2Tag",
                table: "Episode2Tag",
                columns: new[] { "EpisodeId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode2Guest",
                table: "Episode2Guest",
                columns: new[] { "EpisodeId", "GuestId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Podcast_PodcastId",
                table: "Episode",
                column: "PodcastId",
                principalTable: "Podcast",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Guest_Episode_EpisodeId",
                table: "Episode2Guest",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Guest_Guest_GuestId",
                table: "Episode2Guest",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Tag_Episode_EpisodeId",
                table: "Episode2Tag",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episode2Tag_Tag_TagId",
                table: "Episode2Tag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritedEpisode_Episode_EpisodeId",
                table: "FavoritedEpisode",
                column: "EpisodeId",
                principalTable: "Episode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritedEpisode_User_UserId",
                table: "FavoritedEpisode",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaLink_Guest_GuestId",
                table: "SocialMediaLink",
                column: "GuestId",
                principalTable: "Guest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMediaLink_Host_HostId",
                table: "SocialMediaLink",
                column: "HostId",
                principalTable: "Host",
                principalColumn: "Id");
        }
    }
}
