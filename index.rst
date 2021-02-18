Whiteboard API Documentation

---

This API is for the Whiteboard LMS, a Learning Management System

---

ASSIGNMENTS:
All assignments have an internal id

Abstraction: every assignment that inherits from the AbstractAssignment class has the following:

- date assigned
- date due
- dictionary containing the completion status (true/false) of each student
- dictionary containing the grades for each student that has completed the assignment
- stack of comment objects
- points out of value

    Assignments that inherit from AbstractAssignment also inherit from IComparable and can be compared using > and < and ==. This algoritm sorts based on urgency as follows:

- if this assignment object is due sooner than the other
  - if this assignment has a higher point value than the other
    - highest urgency
  - if this assignment has equal point value to the other
    - more urgent
  - if this assignment has less point value than the other
    - slightly less urgent

- if this assignment object is due later than the other
  - if this assignment has a lower  point value than the other
    - lowest urgency
  - if this assignment has equal point value to the other
    - less urgent
  - if this assignment has more point value than the other
    - slightly more urgent
	
- if this assignment object is due the same day as the other
  - if this assignment has higher point value than the other
    - more urgent
  - if this assignment has equal point value to the other
    - same urgency
  - if this assignment has less point value than the other
    - less urgent
	
Assignment Types:

Announcement:

    Does not inherit from AbstractAssignment
	Announcements have a 
	- title
	- content
	- stack of comments
	
	Http requests:
	get by id: GET request 
	/Announcements/{id}
	
	create: POST request
	/Announcements 
	body must be an assignment object in JSON format

	update: PUT request
	/Announcements/{id}
	body must be an assignment object in JSON format
	
	delete: DELETE request
	/Announcements/{id}
	
Poll:
	
	Simple voting system, create 2-10 options and have class members vote on them.
	Voting can lock at a certain date/time - togglable at creation
	
	Does not inherit from AbstractAssignment
	Polls have a
	- title
	- content
	- stack of comments
	- list of PollOption objects that are chooseable by users
	- anonymous/not anonymous toggle

	PollOption: 
		Does not inherit from anything
		PollOptions have an 
		- option
		- vote count 
		- list of voter names (if poll is anonymous then the names simply are not accessed)
	
	
	Requests:
	get by id: /Poll/{id} - GET request

	create: /Poll - POST request
	passes a poll object - note that the ID passed does not end up being the ID of the object.

	delete: /Poll/{id} - DELETE request
	pass the id and if the object exists, it is deleted.

	edit: /Poll/{id} - PUT request
	pass the id and also a Poll object.

	add a vote: /Poll/{id}/{func} - PUT request
	pass the id and a PollVote object.


		PollVote object example: 
		{
			"optName" : "Option One",
			"voter": "Jimmy"
		}

---
Courses
---
	e
=======
This documentation is also available [here]([&lt;no title&gt; &mdash; Whiteboard-LMS latest documentation](https://whiteboard-lms.readthedocs.io/en/latest/))