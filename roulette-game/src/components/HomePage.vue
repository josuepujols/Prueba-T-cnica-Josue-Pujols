<template>
  <v-container class="fill-height">
    <v-responsive
      class="align-centerfill-height mx-auto"
      max-width="900"
    >
      <v-img
        class="mb-4"
        height="150"
        src="@/assets/roulette.png"
      />

      <div class="text-center">
        <div class="text-body-2 font-weight-light mb-n1">Welcome to</div>

        <h1 class="text-h2 font-weight-bold">Roulette Game</h1>
      </div>

      <div class="py-4" />

      <v-row class="py-4">
        <v-col cols="12">
          <v-card
            class="py-4"
            color="surface-variant"
            rounded="lg"
            variant="outlined">
            <template class="d-flex justify-center align-center py-4 full-height">
              <div class=" py-5 ma-2 w-50">
                <v-text-field
                    v-model="playerName"
                    clearable
                    label="Name"
                    prepend-icon="mdi-account"
                ></v-text-field>
                <div class="d-flex justify-center align-center py-4">
                  <v-btn
                    class="ma-2"
                    color="primary"
                    @click="startGame()"
                  >
                    Start or Restart Game
                  <v-icon
                    icon="mdi-gamepad-variant"
                    end
                  ></v-icon>
                </v-btn>
                </div>
              </div>
            </template>

            <v-overlay
              opacity=".12"
              scrim="primary"
              contained
              model-value
            />
          </v-card>
        </v-col>
      </v-row>
    </v-responsive>
  </v-container>
</template>

<script>
import router from "../routes/router.ts";

export default {
  data() {
    return {
      playerName: ''
    };
  },
  methods: {
    startGame() {
      router.push(`/play/${this.playerName}`)
    }
  }
};
</script>
