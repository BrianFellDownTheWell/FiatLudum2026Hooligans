// Roll Cat Dialogue - Zenith
VAR ending = ""

Roll Cat: "Oh golly! Is that my favorite junior baker??? Oh my goodness, it's been so long, I've missed you!" #portrait:rollcat
Roll Cat: "Are you still baking? Oh, I'm sure you are, you were so happy in the kitchen." #portrait:rollcat
Roll Cat: "Oh good heavens, I need to try your macarons again! And your tarts, and your eclairs, and your Baked Alaska of course!" #portrait:rollcat

* [I haven't baked in a while…]
    Roll Cat: "WHAT??? Oh heavens above, you, not baking????? Surely something has gone a-rye with you." #portrait:rollcat
    Roll Cat: "I remember you would come to baking camp every day in the summer and you would beg your parents to let you stay." #portrait:rollcat
    Roll Cat: "You were so gifted that I threw you in with the seasoned bakers." #portrait:rollcat
    Roll Cat: "Well, if you're not baking, how is my favorite animal researcher doing?" #portrait:rollcat
    Roll Cat: "I know you were studying the circadian rhythm of rodents, and you were monitoring the hearts of pelicans, and you were collecting water samples for axolotl environments." #portrait:rollcat
    -> choice_two

* [Were my macarons really that good?]
    Roll Cat: "Oh my stars, they were the best any of my students ever made! Even the ones who I saw go to culinary school." #portrait:rollcat
    Roll Cat: "You knew how to make the shells perfectly! I'm sure you're going to be going to Paris for culinary school too." #portrait:rollcat
    Roll Cat: "Also, how is my favorite animal researcher doing? I know you were studying the circadian rhythm of rodents, and you were monitoring the hearts of pelicans, and you were collecting water samples for axolotl environments." #portrait:rollcat
    -> choice_two


= choice_two

* [I haven't been in the field in a moment…]
    Roll Cat: "Oh lordly lord, WHAT??? You, not in the field??? But you were practically one with nature, how are you not in the field???" #portrait:rollcat
    Roll Cat: "You were so gifted too, you even rediscovered a species of beetle that we were certain went extinct. Why did you stop???" #portrait:rollcat
    Roll Cat: "By all that is holy, what happened to you??? Well, if my favorite animal researcher went extinct, how is my favorite ice skater doing?" #portrait:rollcat
    -> choice_three

* [I didn't really do all that, I was just assisting you.]
    Roll Cat: "By merciful heaven! Don't say that. Even if I was leading the projects, you still were doing so much work." #portrait:rollcat
    Roll Cat: "You relocated an entire population of frogs from a deadly polluted lake. You're so gifted, so talented, you are my prized pupa— err, pupil." #portrait:rollcat
    Roll Cat: "Well, I'm sure my favorite animal researcher is thriving, so, how is my favorite ice skater doing?" #portrait:rollcat
    -> choice_three


= choice_three

* [My last pair of skates don't fit anymore…]
    Roll Cat: "By Jove! You haven't skated in THAT long? But you were so so gifted. Your bunny hops were immaculate, and before you knew it, you mastered your Knee Slide and Mohawk Turns." #portrait:rollcat
    Roll Cat: "Well, if you're not skating anymore, you might as well be dead to me. I spent so long teaching you, and you just forget everything?" #portrait:rollcat
    Roll Cat: "You're my most gifted student, your flame can't die like this. What are you even doing these days?" #portrait:rollcat
    -> choice_four

* [Well, I wouldn't say I was particularly gifted in the first place…]
    Roll Cat: "Goodness me! You were so gifted! Your bunny hops were immaculate, and before you knew it, you mastered your Knee Slide and Mohawk Turns." #portrait:rollcat
    Roll Cat: "I taught you everything that I know and now you're by far my most gifted student!" #portrait:rollcat
    Roll Cat: "So, what else have you been up to these days?" #portrait:rollcat
    -> choice_four


= choice_four

* [Nothing. I'm too tired.]
    Roll Cat: "WHAT. Oh merciful heavens! Nothing??? But you're so talented. You can't let that potential go." #portrait:rollcat
    Roll Cat: "You're so young, you're so wise beyond your years, you're so spirited, you're so passionate, you're so perfect." #portrait:rollcat
    Roll Cat: "HOW ARE YOU TIRED? WAKE UP." #portrait:rollcat
    -> choice_five

* [Surviving.]
    Roll Cat: "Surviving? Oh merciful heavens. Where did your spark go? You're so talented. You can't let that potential go." #portrait:rollcat
    Roll Cat: "You're so young, you're so wise beyond your years, you're so spirited, you're so passionate, you're so perfect." #portrait:rollcat
    Roll Cat: "Why are you surviving when you should be thriving? What is wrong with you?" #portrait:rollcat
    -> choice_five


= choice_five

* [I'm tired.]
    Roll Cat: "How can you be tired? You're so young. You shouldn't be tired. Wake up." #portrait:rollcat
    Roll Cat: "..." #portrait:rollcat
    Roll Cat: "I'm becoming my teacher, aren't I? I apologize, I simply just want you to succeed." #portrait:rollcat
    Roll Cat: "I'm tired too, and I was just hoping my successor could surpass me." #portrait:rollcat
    Roll Cat: "You're talented, I didn't lie about that. It's normal to be tired or to find different things that make you happy." #portrait:rollcat
    Roll Cat: "Even if your next hobby isn't my expertise — even though it probably is, everything is — I'll be there to support you." #portrait:rollcat
    Roll Cat: "Do you need me to pop your balloon? It'll be difficult to find your next passion from up here." #portrait:rollcat

    * * [Pop it.]
        -> end_pop
    * * [Later.]
        -> end_later

* [Leave me the fuck alone. I'm leaving.]
    Roll Cat: "Why? You're so talented, I'm just trying to help you realize your full potential. Stop being lazy! Wake up!" #portrait:rollcat
    Roll Cat: "..." #portrait:rollcat
    Roll Cat: "I'm becoming my teacher, aren't I? I apologize, I simply just want you to succeed." #portrait:rollcat
    Roll Cat: "I'm tired too, and I guess I was just hoping my successor could surpass me. You're talented, I didn't lie about that." #portrait:rollcat
    Roll Cat: "It's normal to be tired or to find different things that make you happy. Maybe, by pushing you so hard, I stopped you from reaching your potential." #portrait:rollcat
    Roll Cat: "My sincerest apologies. I'm sure you'll go on to do amazing things once you take care of yourself." #portrait:rollcat
    Roll Cat: "Goodbye." #portrait:rollcat
    -> end_later


= end_pop
~ ending = "pop"
// Pop it ending
-> END

= end_later
~ ending = "later"
// Later ending
-> END