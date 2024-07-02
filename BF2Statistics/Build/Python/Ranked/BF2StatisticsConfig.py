# ------------------------------------------------------------------------------
# BF2Statistics 2.3.0 - Config File
# ------------------------------------------------------------------------------
# Conventions:
#    0 -> Disable
#    1 -> Enable
# ------------------------------------------------------------------------------

# ------------------------------------------------------------------------------
# Debug Logging
# ------------------------------------------------------------------------------
debug_enable = 0
debug_log_path = 'python/bf2/logs'		# Relative from BF2 base folder
debug_fraglog_enable = 0				# Detailed 'Fragalyzer' Logs (requires existing folder "mods/<ModName>/logs/")

# ------------------------------------------------------------------------------
# Statistics Enabling
# ------------------------------------------------------------------------------
# 0 = disable statistics, 1 = enable statistics (requires an ASP stats server)
# By disabling the stats, this server will be "non-ranked"
# ------------------------------------------------------------------------------
stats_enable = 1

# ------------------------------------------------------------------------------
# ASP Stats Backend Web Server
# ------------------------------------------------------------------------------
http_backend_addr = '127.0.0.1'
http_backend_port = 80
http_backend_asp = '/ASP/bf2statistics.php'

# ------------------------------------------------------------------------------
# ASP Stats Backup Central Community Web Server (OPTIONAL)
# ------------------------------------------------------------------------------
# Note: this capability is for linking stats data to a centrally maintained STATS #
#   server.  This could be useful for those who are running a LAN or Tournament   #
#   (with local STATS), but wish to have this data credited to their normal STATS #
#   DB. There are three (3) options for the central DB update:                    #
#     0: Disabled - 'nuff said!                                                   #
#     1: Sync  - This simply copies the SNAPSHOT as-is to the central DB          #
#     2: Minimal - Record everything, except Rank & Award data (typically use     #
#				for LANs or Tournaments where local db starts blank)              #
# ------------------------------------------------------------------------------
http_central_enable = 0				# Use settings above (0, 1, 2)
http_central_addr = '192.168.1.102'	# Address Central STATS Server
http_central_port = 80
http_central_asp = '/ASP/bf2statistics.php'

# ------------------------------------------------------------------------------
# Snapshot Logging
# ------------------------------------------------------------------------------
# Enables server to make snapshot backups. 
# 0 = log only on error sending to backend
# 1 = all snapshots 
# ------------------------------------------------------------------------------
snapshot_logging = 0
snapshot_log_path_sent = 'python/bf2/logs/snapshots/sent' 		# Relative from the BF2 base folder
snapshot_log_path_unsent = 'python/bf2/logs/snapshots/unsent' 	# Relative from the BF2 base folder

# ------------------------------------------------------------------------------
# Snapshot Prefix
# ------------------------------------------------------------------------------
# Prefix Snapshots with this tag. This is also your Server's Prefix. Its a good idea
# to make this prefix unique to each server that may be posting snapshots to an
# ASP back end, as it can assist with distinguishing which server sent which snapshot
# ------------------------------------------------------------------------------
snapshot_prefix = 'svr1'

# ------------------------------------------------------------------------------
# Medals Processing
# ------------------------------------------------------------------------------
# Suffix for your custom medals file(s). 
# Example: A profile named "custom" = medal_data_custom.py
# ------------------------------------------------------------------------------
medals_custom_data = ''
# A list of mods that xpack (special forces) medals can be earned while playing
# Example: ['mods/xpack', 'mods/bf2', 'mods/ModName'] (all entries must be lower case!!)
medals_xpack_mods = ['mods/xpack']

# ------------------------------------------------------------------------------
# Player Manager
# ------------------------------------------------------------------------------
# Local IP address for AI Bots
# ------------------------------------------------------------------------------
pm_ai_player_addr = '127.0.0.1'		# Not recommended to change

# ------------------------------------------------------------------------------
# Clan Manager
# ------------------------------------------------------------------------------
enableClanManager = 0	# Use the Clan Manager to control Access to your server!
serverMode = 0
	# Mode 1: Clan ONLY (Must be on Clan List to Join Server)
	# Mode 2: Priority Proving Grounds (Clan Members AND those players that meet
	#	the minimum requirements.Clan Members get priority)
	# Mode 3: Proving Grounds (Clan Members AND those players that meet the
	#	minimum requirements)
	# Mode 4: Experts ONLY (Only players that meet the minimum requirements)
	# Mode 0: Public (Free-for-All. No Restrictions!)

# Clan Manager Criteria
criteria_data = (
		('clantag', ''),	# Clan Tag (Matches First Part of Player Name, used for Whitelist)
		('score', 0),		# Minimum Global Score
		('rank', 0),		# Minimum Global Rank
		('time', 0),		# Minimum Global Time Played
		('kdratio', 0),		# Minimum Global Kill/Death Ratio
		('country', ''),	# Registered Country of Origin Code (Seperate multiple by comma ',')
		('banned', 0),	# Maximum banned count! PermBan is ALWAY BlackListed
	)

# ------------------------------------------------------------------------------
# END CONFIGURATION
# ------------------------------------------------------------------------------