Reference: http://wiki.integryst.com/display/products/UserAuthSourceFlipper

# NAME
Integryst UserAuthSourceFlipper

# INTENDED PLATFORMS
Aqualogic User Interaction 6.1 and above  
Oracle WebCenter Interaction 10gR3 and above  
Microsoft SQL Server and Oracle

# DESCRIPTION
Every user and group in WCI has an object ID.  When a new Authentication Source is introduced with the same users (such as when migrating from AD to LDAP), it is necessary to swap the object IDs of users and groups in order to preserve security settings and group membership.  This code uses direct database connections to swap object IDs of users and group.

# BUILD
This project is built with Microsoft Visual Studio 2008.

# LICENSE
This project is licensed under the MIT open-source license; see license.txt