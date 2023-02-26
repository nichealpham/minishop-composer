<template>
  <div class="camera_wrapper" v-show="show">
    <center-content>
      <template v-slot>
        <video ref="video" id="video" v-show="showVideo" autoplay></video>
        <canvas ref="canvas" id="canvas" v-show="!showVideo"></canvas>
      </template>
    </center-content>
    <v-btn
      class="mx-2 btn_capture"
      fab
      large
      @click="capture"
      :loading="uploading"
    >
      <v-icon dark>
        mdi-camera-party-mode
      </v-icon>
    </v-btn>
    <v-icon dark class="btn_close" @click="close">
      mdi-close
    </v-icon>
  </div>
</template>

<script>
import { firebaseUploadBase64 } from "@/plugins/firebase";
import { uuidv4 } from "@/plugins/helpers";
import CenterContent from "@/components/CenterContent.vue";

export default {
  components: { CenterContent },
  props: {},
  watch: {},
  mounted() {},
  data() {
    return {
      uploading: false,
      show: false,
      showVideo: true,
      video: {},
      canvas: {},
    };
  },
  methods: {
    start() {
      this.show = true;
      this.showVideo = true;
      this.video = document.getElementById("video");
      this.canvas = document.getElementById("canvas");
      if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices
          .getUserMedia({ video: { facingMode: "environment" }, audio: false })
          .then((stream) => {
            this.video.srcObject = stream;
            this.video.play();
          });
      }
    },
    stop() {
      const mediaStream = this.video.srcObject;
      const tracks = mediaStream.getTracks();
      tracks.forEach((track) => track.stop());
    },
    close() {
      if (this.video.pause) {
        this.video.pause();
        this.video.currentTime = 0;
      }
      this.video = {};
      this.canvas = {};
      this.show = false;
      this.showVideo = true;
    },
    async capture() {
      this.canvas.width = this.video.videoWidth;
      this.canvas.height = this.video.videoHeight;
      var context = this.canvas.getContext("2d");
      context.drawImage(this.video, 0, 0);
      this.showVideo = false;
      var base64Str = this.canvas.toDataURL();
      this.uploading = true;
      var fileName = `${Date.now()}-${uuidv4()}.png`;
      var downloadURL = await firebaseUploadBase64(base64Str, fileName);
      this.uploading = false;
      this.$emit("change", downloadURL);
      this.close();
    },
  },
};
</script>

<style lang="scss">
.camera_wrapper {
  width: 100vw;
  height: 100vh;
  position: fixed;
  z-index: 100;
  top: 0px;
  right: 0px;
  background-color: black;

  #video {
    width: 100vw;
  }
  #canvas {
    width: 100vw;
  }
  .btn_capture {
    left: calc(50vw - 50px);
    bottom: 20px;
    position: fixed;
  }
  .btn_close {
    right: 20px;
    top: 20px;
    position: fixed;
  }
}
</style>
