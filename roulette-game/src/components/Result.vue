<template>
  <v-card class="w-100 pa-4" outlined>
    <v-card-title>
      <h2 class="text-h5 font-weight-bold text-center py-3">Results</h2>
    </v-card-title>
    <v-card-text class="py-5">
      <v-col>
        <v-row>
          <span>Roulette Number: <strong>{{ this.rouletteNumber }}</strong></span>
        </v-row>
        <v-row>
          <span>Roulette Color: <strong>{{ this.rouletteColor }}</strong></span>
        </v-row>
        <v-row>
          <span v-if="this.evenOrOdd != null">Roulette Even or Odd: <strong>{{ this.rouletteEvenOrOdd }}</strong></span>
        </v-row>
        <v-row>
          <span v-if="this.number != null"
            >Your Number: <strong>{{ this.number }}</strong></span
          >
        </v-row>
        <v-row>
          <span v-if="this.evenOrOdd != null"
            >Your Even or Odd Guess:
            <strong>{{ this.evenOrOdd }}</strong></span
          >
        </v-row>
        <v-row>
          <span
            >Your Color: <strong>{{ this.color }}</strong></span
          >
        </v-row>
      </v-col>

      <v-row class="mb-4 my-3">
        <v-col>
          <span
            v-if="
              this.color != null &&
              this.number == null &&
              this.evenOrOdd == null
            "
          >
            Operation Type: <strong>Color</strong>
          </span>
          <span
            v-if="
              this.color != null &&
              this.number != null &&
              this.evenOrOdd == null
            "
          >
            Operation Type: <strong>Number And Color</strong>
          </span>
          <span
            v-if="
              this.color != null &&
              this.number == null &&
              this.evenOrOdd != null
            "
          >
            Operation Type: <strong>Even or Odd And Color</strong>
          </span>
        </v-col>
      </v-row>
      <v-divider></v-divider>
      <v-row class="mb-4 py-4 d-flex ">
        <v-col>
          New Amount Result: {{ result }}
          <strong>
            <p v-if="this.isWin">Congratulations, you won the bet!</p>
            <p v-if="!this.isWin">Sorry, you lost the bet!</p>
          </strong>
        </v-col>
        <v-col>
          <v-btn @click="saveBet" color="green">
            Save Result
            <v-icon icon="mdi-content-save" end></v-icon>
          </v-btn>
        </v-col>
      </v-row>
    </v-card-text>
    <p class="text-center">Bet placed by {{ this.playerName }}</p>
  </v-card>
</template>

<script>
import { RouletteService } from '../services/RouletteService.ts';
import router from "../routes/router.ts";

export default {
  data() {
    return {
      result: 0,
      isWin: null,
      rouletteColor: '',
      rouletteNumber: '',
      rouletteEvenOrOdd: ''
    };
  },
  props: {
    number: {
      type: Number,
      required: true,
    },
    evenOrOdd: {
      type: String,
      required: true,
    },
    color: {
      type: String,
      required: true,
    },
    playerName: {
      type: String,
      required: true,
    },
    bet: {
      type: Number,
      required: true,
    },
    amount: {
      type: Number,
      required: true,
    },
  },
  mounted() {
    this.makeBet();
  },

  methods: {
    async makeBet() {
      const service = new RouletteService();

      // Get the values from the roulette
      const roulette = await service.getNumberAndColor();
      if (roulette.number % 2 == 0) {
        this.rouletteEvenOrOdd = 'Even';
      }
      else {
        this.rouletteEvenOrOdd = 'Odd';
      }

      this.rouletteColor = roulette.color;
      this.rouletteNumber = roulette.number;

      if (this.color != null && this.number == null && this.evenOrOdd == null) {
        var response = await service.placeBetByColor(this.color, roulette.color, this.bet, this.amount);
        this.result = response.amount;
        this.isWin = response.isWin;
      } else if (this.color != null && this.number != null && this.evenOrOdd == null) {
        var response = await service.placeBetByNumber(this.number, roulette.number, this.color, roulette.color, this.bet, this.amount);
        this.result = response.amount;
        this.isWin = response.isWin;
      } else {
        const evenOrOddBool = this.evenOrOdd == "Odd" ? true : false;
        var response = await service.placeBetByEvenOrOdd(evenOrOddBool, roulette.number, this.color, roulette.color, this.bet, this.amount);
        this.result = response.amount;
        this.isWin = response.isWin;
      }
    },
    async saveBet() {
      const service = new RouletteService();
      var response = await service.saveBet(this.playerName, this.result);
      if (response) {
        alert("Bet saved successfully");
        router.push(`/home`);
      }
      else {
        alert("Error saving bet");
      }
    }
  }
};
</script>
