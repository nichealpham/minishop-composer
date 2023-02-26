<template>
  <div>
    <div v-show="!isShow">
      <img class="img_book_cover" src="icons/book.png" />
      <p class="label_book_cover">
        {{ this.message || $t("common.selecttoview") }}
      </p>
    </div>
    <div v-show="isShow">
      <slot></slot>
    </div>
  </div>
</template>

<script>
export default {
  props: ["message"],
  data() {
    return {
      isShow: false,
    };
  },
  mounted() {
    this.isShow = false;
  },
  methods: {
    showContent() {
      this.isShow = true;
    },
    showCover() {
      this.isShow = false;
    },
  },
};
</script>

<style lang="scss">
.img_book_cover {
  width: 120px;
  margin-left: calc(50% - 60px);
}
.label_book_cover {
  font-size: 0.95rem;
  width: 100%;
  display: block;
  text-align: center;
}
</style>
