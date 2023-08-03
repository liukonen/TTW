# ⛔️ DEPRECATED TTW
Microsoft Cognitive Services Text Translation wrapper
This is a wrapper DLL for Microsoft's new Cognitive Services API, when working with the text translation service. 

## How to use.
In the constructor of the object you will need to pass in the key value that MS gives you on there translation service. The translate call takes in the text, the from language, which is the language that the text is currently in, and a to language, which is what the output language that Microsoft will translate to.

## reason behind it
I participated in the Metropolitan Milwaukee Association of Commerce COSBE’s Be The Spark Tour for my company. For this event, we needed to demonstrate how our company uses technology to enhance the travel experience. I knew ahead of time that the school participating for our company was a multilingual immersion school, with some students having spanish as a primary language.  For a demo, I found the source code of an AIML based chatbot, and used a winform to give the students an input and output they could interact with.

For the cognitive service side, I need to do the following,
+ Take the input text, and determine what language the user was using
+ Translate the text to english
+ Send the text to the chatbot
+ Receive the text, output the english response
+ Translate the response back to the language the user was originally using
+ Output the translated text

 The Cognitive API logic I developed at home on my own time, and since it is not part of our core logic, I still had the rights to publish this myself.

Im sure that a future version of VS will make binding to there cognitive services or rest api's much easier, like they did web services... but until they do, we have this :) (or this is a future project for myself

# Learnings
+ REST API
+ Cogintive Services
+ tokenRequest
+ Http Header requests
+ Azure

