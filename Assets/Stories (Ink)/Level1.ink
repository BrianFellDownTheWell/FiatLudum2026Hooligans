// Bird Dialogue - Zenith

Bird: "Hey, it's been a while, hasn't it?" #portrait:bird

* [How long has it been?]
    Bird: "I'm not sure. I've been counting how many times the moon came and went, but after a certain point I lost track." #portrait:bird
    Bird: "How have you been?" #portrait:bird
    -> choice_two

* [You shouldn't be here.]
    Bird: "Really? If anything, you shouldn't be here. Your balloon doesn't seem to be doing too great, I'm shocked you made it this high." #portrait:bird
    Bird: "How have you been?" #portrait:bird
    -> choice_two


= choice_two

* [Good.]
    Bird: "I don't believe you. You're still a terrible liar, just like the old days." #portrait:bird
    Bird: "Remember when we used to play that detective card game together? Every time you were the criminal, I swear your voice would go up an octave." #portrait:bird
    Bird: "I miss our time together. I wish we could spend every moment together again!" #portrait:bird
    Bird: "Do you think we'll ever see each other again?" #portrait:bird
    -> choice_three

* [I've been better.]
    Bird: "I can tell. I guess it's my turn to help you! I need to repay you for all of the times you would pick me up to get milkshakes whenever I felt like I needed to step away from home for a bit." #portrait:bird
    Bird: "Even though those were scary nights, I still miss them. I wish we could spend practically every night together again!" #portrait:bird
    Bird: "Do you think we'll ever have another us night again?" #portrait:bird
    -> choice_three


= choice_three

* [We will. I promise.]
    Bird: "I knew I could count on you. I don't even know why I asked, of course we would." #portrait:bird
    Bird: "You know, I need to think of you more like the moon. Even though sometimes I look up and I can't find you, I know you're still there and I'll see you again soon." #portrait:bird
    Bird: "I always felt like whenever you left, it would be the last time I ever saw you, so I guess I just never let you leave. I'm sorry." #portrait:bird
    Bird: "Thank you for all the sacrifices you've made for me. I'm getting pretty tired, I don't think my wings were meant for flying this high." #portrait:bird
    Bird: "I know you're going to keep rising to greater heights than I could even dream of, but please don't forget your promise." #portrait:bird
    Bird: "Come back to Earth every once in a while, okay? If you really want me to, I can pop your balloon and you can come back to Earth now. You seem a bit tired, you could use the rest." #portrait:bird

    * * [Pop it.]
        -> end_pop
    * * [Later.]
        -> end_later

* [We can't. We stop each other from going where we need to.]
    Bird: "Oh. I guess that's true. I'm sorry, I must have really dragged you down all those years…" #portrait:bird
    Bird: "You were the best friend I ever had, and I know you sacrificed a lot of your time to take care of me. It was definitely too much time." #portrait:bird
    Bird: "I fly parallel to the world, but you need to keep rising from it to reach your destination." #portrait:bird
    Bird: "Maybe I'm selfish— no, I definitely am, but please at least remember me every once in a while?" #portrait:bird
    Bird: "If we can't be together on this journey, the fact that our journeys crossed paths is something we can celebrate." #portrait:bird
    Bird: "I know I will celebrate." #portrait:bird
    -> end_later


= end_pop
// Pop it ending
-> END

= end_later
// Later ending
-> END