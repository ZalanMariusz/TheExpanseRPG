BEGIN TRANSACTION;
DROP TABLE IF EXISTS "Drive";
CREATE TABLE IF NOT EXISTS "Drive" (
	"DriveName"	TEXT,
	"DriveDescription"	TEXT NOT NULL,
	"DriveQuality"	TEXT NOT NULL,
	"DriveDownfall"	TEXT NOT NULL,
	PRIMARY KEY("DriveName")
);
DROP TABLE IF EXISTS "DriveTalent";
CREATE TABLE IF NOT EXISTS "DriveTalent" (
	"DriveName"	TEXT,
	"TalentName"	TEXT,
	FOREIGN KEY("DriveName") REFERENCES "Drive"("DriveName"),
	FOREIGN KEY("TalentName") REFERENCES "Talent"("TalentName"),
	PRIMARY KEY("DriveName","TalentName")
);
INSERT INTO "Drive" ("DriveName","DriveDescription","DriveQuality","DriveDownfall") VALUES ('Achiever','You want to accomplish things, ideally lasting things for which you will be recognized. Some achievers want success, fame, and accolades, while others are looking to leave a legacy, something for which they will be remembered. But all achievers are keenly aware that life is short and everyone has the same number of hours in a day; it’s how you spend them that matters.','Ambition, knowing what you want and going after it.','Obsession, becoming too focused on your goals and unable to see anything (or anyone) else.'),
 ('Builder','You want to create something lasting. It might be an institution, an organization, a movement, a community, or something else. Unlike the Achiever, who is all about the accomplishment, you’re all about the end product, and willing to do what it takes, for as long as it takes, to get there.','Organization, being able to figure out how to structure things so they work.','Stubbornness, becoming so caught up in structure that you lose flexibility.'),
 ('Caregiver','You’re here to help as many people as you can, however you can. You might not be able to help everyone, but you are certainly going to try, and you have a difficult time turning away from anyone in need.','Compassion, naturally feeling and responding to others needs.','Self-sacrifice, a tendency to place the needs and even physical safety of others above your own.'),
 ('Ecstatic','Life is a banquet and most poor suckers are starving to death! Not you, though You aim to squeeze every drop of juice out of your limited time in this world, and you generally encourage your friends to join you, although you’re willing to go off on your own if none of them do.','Zest for life and a general willingness to find enjoyment in things and try new experiences.','Irresponsibility, a tendency to overdo enjoyment at the expense of more practical matters.'),
 ('Judge','Life is all about making decisions and exercising good judgment. You believe in finding out as much as you can about things so you can make informed and carefully considered judgements about them.','Discernment, paying close attention to details and information.','Aloofness, a tendency to distance yourself from the world around you to remain objective.'),
 ('Leader','Somebody needs to stand up, take responsibility, and get things done. You might relish the opportunity to lead or take it up reluctantly, but either way, you’re a natural at it and it’s hard to resist an opportunity to take charge.','Responsibility, the ability to take on and make decisions and live with the outcome.','Isolation, the distance imposed by your role as leader, which can affect relationships and how close you can be with people you lead.'),
 ('Networker','You’re here to make friends, because it is all about who you know. You may be a genuine “people person” with a knack for making a good first impression or a first-class manipulator who understands how to get what you want from people.','Gregariousness. You’re good with people and at home in social situations, and tend to seek them out.','Overwrought. You tend to get caught up in social conflicts, and think finding just the right person is the solution to every problem, making you prone to overly complex schemes.'),
 ('Penitent','You screwed up. Maybe you didn’t mean to, or maybe you did and should have known better. Whatever the case, you’re trying to make it right now by doing better. You may or may not want anyone else to know about your past mistakes, but what happens next is what really matters.','Humility. You have fallen low and learned from it, so you’re not quick to judge or to accept accolades.','Guilt, as you’re sometimes haunted by your past mistakes and feel any new missteps heavily.'),
 ('Protector','There are a lot of threats out in the world, and you are going to guard against them. Exactly what you consider a threat, and who or what you are protecting from it, might vary but the most important thing is you are not going to stand idly by when you could act.','Devotion to those under your protection and to your ideals, no matter what challenges lie in your path.','Recklessness when it comes to putting yourself (and even others) in harm’s way to protect your charges.'),
 ('Rebel','Authority needs to be questioned. You may think all forms of authority are inherently oppressive and need to be brought down, or just that healthy institutions require periodic house-cleanings—or purging fires. Whatever the case, you threw out doing things “by the book” some time ago.','Innovation, the ability to look at things from angles no one else has considered.','Defiance, a dislike of conformity, conventionality, and doing what you’re told.'),
 ('Survivor','Life is hard, but you are going to make it, no matter what. You may have already had to struggle to survive in your early life, or you are simply preparing for the struggle you know is coming—whether anyone else believes it or not.','Preparedness. You survive by being ready for anything and knowing what to do in any situation.','Cynicism. You are always anticipating and preparing for the worst, such that it’s difficult for you to see the good in anything.'),
 ('Visionary','You have a vision to share with the world, sometimes whether the world wants it or not. This vision might be your unique artistic expression, a personal philosophy, or religious or spiritual gnosis, but you’re driven to share it, regardless of the risks.','Faith in your vision and its ability to reach the right people, given the opportunity.','Zealotry. Your vision becomes confused with absolute truth, which might lead you to offer it where it is unwelcome or to try and eliminate other visions you see as false or competition. Alternatively, your downfall is Doubt, where you experience a crisis of faith and are uncertain how to follow your vision—or if you should continue to follow it at all.');
INSERT INTO "DriveTalent" ("DriveName","TalentName") VALUES ('Achiever','Expertise(Focus)'),
 ('Achiever','Inspire'),
 ('Builder','Maker'),
 ('Builder','Oratory'),
 ('Caregiver','Inspire'),
 ('Caregiver','Medic'),
 ('Ecstatic','Attractive'),
 ('Ecstatic','Carousing'),
 ('Judge','Knowledge'),
 ('Judge','Observation'),
 ('Leader','Command'),
 ('Leader','Inspire'),
 ('Networker','Contacts'),
 ('Networker','Intrigue'),
 ('Penitent','Fringer'),
 ('Penitent','Know-It-All'),
 ('Protector','Misdirection'),
 ('Protector','Protector'),
 ('Rebel','Expertise(Focus)'),
 ('Rebel','Improvisation'),
 ('Survivor','Fringer'),
 ('Survivor','Tactical Awareness'),
 ('Visionary','Artistry'),
 ('Visionary','Oratory'),
 ('Visionary','Performance');
COMMIT;
