<template>
  <h2 class="text-h5 font-weight-bold text-center py-5">
    Player: <strong>{{ playerName }}</strong>
  </h2>
  <v-container class="d-flex justify-center w-100 py-5">
    <v-btn @click="goHome()" color="orange-darken-2">
      <v-icon icon="mdi-arrow-left" end></v-icon>
      Back
    </v-btn>
    <v-row class="pa-4 w-50 d-flex flex-column">
      <v-card class="pa-4 w-100" outlined>
        <v-card-text class="py-5">
          <v-row class="mb-4">
            <v-col>
              <v-text-field
                v-model="amount"
                type="number"
                label="Amount"
                prepend-icon="mdi-currency-usd"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row class="mb-4">
            <v-col>
              <v-text-field
                v-model="bet"
                type="number"
                label="Bet"
                prepend-icon="mdi-currency-usd"
                :error-messages="betError"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row class="mb-4">
            <v-col>
              <v-select
                v-model="selectedNumber"
                :items="numbers"
                label="Select a Number"
                prepend-icon="mdi-numeric"
                :disabled="iEvenOddGuess"
              ></v-select>
              <v-select
                v-model="selectedValue"
                :items="numberOptions"
                label="Select Even or Odd"
                prepend-icon="mdi-numeric"
                :disabled="isNumberGuess"
              ></v-select>
            </v-col>
          </v-row>
          <v-row class="mb-4">
            <v-col>
              <v-radio-group v-model="selectedColor" label="Select Color">
                <v-radio label="Black" value="Black"></v-radio>
                <v-radio label="Red" value="Red"></v-radio>
              </v-radio-group>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="d-flex justify-center">
              <v-btn
                class="mr-4"
                @click="placeBet"
                color="primary"
                :disabled="!canPlaceBet"
              >
                Place Bet
                <v-icon icon="mdi-currency-usd" end></v-icon>
              </v-btn>
              <v-btn @click="resetGame" color="red">
                Reset Game
                <v-icon icon="mdi-reload" end></v-icon>
              </v-btn>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-row>

    <v-row v-if="showResult" class="pa-4 w-50 d-flex flex-column">
      <Result
        :number=this.selectedNumber
        :evenOrOdd=this.selectedValue
        :color=this.selectedColor
        :playerName=this.playerName
        :bet=this.bet
        :amount=this.amount
      />
      <v-card class="w-100 pa-5 mt-8" outlined>
        <v-card-title>
          <h2 class="text-h5 font-weight-bold text-center py-3">Legend</h2>
        </v-card-title>
        <v-card-text>
          <ul>
            <li>
              As long as the user does not win the bet, he will lose the amount
              bet.
            </li>
            <li>
              If the user bets on a heat and gets it right, he will win half of
              the amount of his bet.
            </li>
            <li>
              If the user bets on the odd or even numbers of a given color and
              wins, he will win an amount equal to the amount bet.
            </li>
            <li>
              If the user bets on a specific number and color and gets it right,
              he will win triple the amount bet.
            </li>
          </ul>
        </v-card-text>
      </v-card>
    </v-row>
  </v-container>
</template>

<script>
import router from "../routes/router.ts";
import { useRoute } from "vue-router";
import { RouletteService } from '../services/RouletteService.ts';
import { ref, onMounted, watch } from 'vue';

export default {
  setup() {
    const route = useRoute();
    const name = route.params.playerName;
    const amount = ref(0);

    onMounted(async () => {
      const service = new RouletteService();
      const player = await service.getUserAmount(name);
      amount.value = player.amount;
    });

    return {
      playerName: name,
      amount: amount
    };
  },
  data() {
    return {
      bet: 0,
      betError: "",
      selectedNumber: null,
      selectedValue: null,
      numbers: [],
      numberOptions: ["Even", "Odd"],
      selectedColor: null,
      showResult: false,
      //Values from the service result
      numberRecieved: null,
      colorRecieved: null,
    };
  },
  computed: {
    canPlaceBet() {
      return (
        this.bet && this.selectedColor != null
      );
    },
    isNumberGuess() {
      return this.selectedNumber !== null;
    },
    iEvenOddGuess() {
      return this.selectedValue !== null;
    },
  },
  methods: {
    resetGame() {
      this.bet = 0;
      this.betError = "";
      this.selectedNumber = null;
      this.selectedValue = null;
      this.selectedColor = null;
      this.showResult = false;
      this.updateNumbers();
    },
    updateNumbers() {
      this.numbers = Array.from({ length: 37 }, (_, i = 0) => i);
    },
    placeBet() {
      if (this.canPlaceBet) {
        this.showResult = true;
      }
    },
    goHome() {
      const userResponse = confirm(
        "You will loss all this match, do you want to exit anyway?"
      );
      if (userResponse) {
        router.push("/home");
      }
    }
  },
  mounted() {
    this.updateNumbers();
  },
};
</script>

<style scoped>
.v-card {
  max-width: 500px;
  margin: auto;
}
</style>
