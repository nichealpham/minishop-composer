<template>
  <div class="backdrop_blur">
    <div
      class="container_background"
      :style="{
        'background-image': backgroundImage,
      }"
    >
      <!-- <div class="container_background"> -->
      <div class="blur_filter"></div>
    </div>
    <div
      class="container_page"
      transition="fade-transition"
      v-show="!hideMainApp"
    >
      <slot></slot>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  data() {
    return {};
  },
  computed: {
    ...mapGetters({
      backgroundImage: "Theme/getBackground",
    }),
    ...mapGetters("MiniApp", ["hideMainApp"]),
  },
  methods: {
    showApp() {
      this.verified = true;
    },
  },
};
</script>
<style lang="scss">
.backdrop_blur {
  .container_background {
    position: fixed;
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    width: 100vw;
    height: 100vh;
    min-width: 360px;
    .blur_filter {
      position: fixed;
      width: 100vw;
      height: 100vh;
      min-width: 360px;
      // background-color: rgba(0, 0, 0, 0.15);
      background-color: rgba(228, 232, 246, 0.1);
    }
  }
  .container_page {
    width: 100vw;
    height: 100vh;
    position: fixed;
    overflow-x: hidden;
    overflow-y: scroll;
    max-width: 1366px;
    min-width: 360px;
  }
  @media screen and (min-width: 1366px) {
    .container_page {
      margin-left: calc(50vw - 683px);
    }
  }
}
</style>
