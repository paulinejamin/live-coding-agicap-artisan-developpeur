# Welcome to the support for our live coding!

We are Pauline Jamin and Caroline Desplanques, working on a live coding in partnership with Artisan Developpeur.

Our goal is to share some insights on how we work currently at Agicap, being in the pairing flow, in our DDD mindset, and maybe show a little bit of an architecture we are fond of: hexagonal architecture.

The idea of this project is : we are many devs all around Europe and when devs are coming to Lyon, we like to celebrate and go to a bar. 
So we did a fictionnal little API to find the best date and bar to gather developers. The project was done a long time ago in n-tier architecture and did not evolve since.

On `main` you will find the base code.  
You'll find the video record on [Artisan Developpeur Website](https://compagnon.artisandeveloppeur.fr/veille/youtube-live-coding-comment-passer-d-un-bout-de-code-crade-a-un-code-facile-a-faire-evoluer)

## Product requirements
### Rule #1: boats
Summer is coming and we would like to add a new data source for boat bars in order to chill next to the Rhone river. If possible, boats are always our first choice and must be booked instead of an indoor bar.
Try to implement this without refactoring. Then do as much refactoring as needed.

### Rule #2: bar capacity
Bars are complaining that we fill up to much space. we would like to book only bars where we fill less than 80% of their capacity.


### Rule #3: rooftops
We are happy we added boats, but we forgot about rooftops ! it is another data source.


## Conclusion
 On `proposition-one` you will find one proposition where we went towards a more DDD-like approach. 
 
 It is far from perfect, but it allows us to implement rules 2 and 3 much more easily.
